using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DK.Domain.DTO.Security.PermissionGroups;
using DK.Domain.Entity.Security;
using DK.Service.Interface.Repository;
using DK.Utility;

namespace DK.Service.Service.Security
{
    public interface IPermissionService
    {
        Task<bool> UpdateList(List<PermissionGroupVM> xPermissionGroups);
        Task<bool> EditRolePermission(long xRoleID, List<long> xSelectedPermissionIDs);
        Task<IEnumerable<GetPermissionGroupVM>> GetsForRole(long xID);
    }

    public class PermissionService : BaseService, IPermissionService
    {
        #region == Ctor ==
        public PermissionService(IUnitOfWork uow, IMapper IMapper) : base(uow, IMapper)
        { }
        #endregion

        #region   == Read Data ==
        public async Task<IEnumerable<GetPermissionGroupVM>> GetsForRole(long xRoleID)
        {
            var xPermissionGroups = (await uow.PermissionGroupRepository.GetAsync<GetPermissionGroupVM>()).Where(w => w.xParentID == null);

            // مقدار دهی فیلد ایز سلکتد به صورت بازگشتی
            UpdateIsSelected(xPermissionGroups, xRoleID);
            return xPermissionGroups;
        } 
        #endregion

        #region == Put Data ==
        public async Task<bool> EditRolePermission(long xRoleID, List<long> xSelectedPermissionIDs)
        {
            bool xResult = true;
            List<string> xerrors = new List<string>();
            Role xDbRole = (await uow.RoleRepository.GetAsync(x => x.Id == xRoleID, null, "xPermissionRoles")).FirstOrDefault();
            if (xDbRole == null)
            {
                throw new ErrorMessageException($"نقش انتخابی نا معتبر می باشد");
            }

            IEnumerable<long> xDBPermissionIDs = xDbRole.xPermissionRoles.Select(d => d.xPermissionID);
            IEnumerable<long> xPermissionIDsForDelete = xDBPermissionIDs.Except(xSelectedPermissionIDs);
            List<PermissionRole> xPermissionRoleForDelete = xDbRole.xPermissionRoles.Where(d => xPermissionIDsForDelete.Contains(d.xPermissionID)).ToList();
            List<long> xPermissionIDsForAdd = xSelectedPermissionIDs.Except(xDBPermissionIDs).ToList();
            xPermissionIDsForAdd?.ForEach(xPermissionRole =>
            {
                xDbRole.xPermissionRoles.Add(new PermissionRole
                {
                    xPermissionID = xPermissionRole,
                    xRoleID = xRoleID
                });
            });
            xPermissionRoleForDelete?.ForEach(xPermissionRole =>
            {
                xDbRole.xPermissionRoles.Remove(xPermissionRole);
            });
            uow.RoleRepository.Update(xDbRole);
            await uow.SaveChangesAsync();
            return xResult;
        }

        private void UpdateIsSelected(IEnumerable<GetPermissionGroupVM> xPermissionGroups, long xRoleID)
        {
            foreach (var group in xPermissionGroups)
            {
                if (group.xPermissionGroups.Any())
                {
                    UpdateIsSelected(group.xPermissionGroups, xRoleID);
                }
                foreach (var perm in group.xPermissions)
                    perm.xIsSelected = perm.xPermissionRoles.Any(a => a.xRoleID == xRoleID);
            }
        }

        public async Task<bool> UpdateList(List<PermissionGroupVM> xList)
        {
            var xDBPermissions = await uow.PermissionRepository.GetAsync();
            var xDBPermissionGroups = await uow.PermissionGroupRepository.GetAsync();

            foreach (var xGroup in xList)
            {
                var xPermissionGroup = xDBPermissionGroups.FirstOrDefault(a => a.xControllerName == xGroup.xControllerName);

                if (xPermissionGroup == null) // New Group 
                {
                    var xNewGroupDB = new PermissionGroup { xName = xGroup.xName, xControllerName = xGroup.xControllerName };
                    uow.PermissionGroupRepository.Insert(xNewGroupDB);

                    foreach (var xItem in xGroup.xPermissions)
                    {
                        var xPermission = xDBPermissions.FirstOrDefault(a => a.xAction == xItem.xAction && a.xController == xGroup.xControllerName);

                        if (xPermission == null)
                            uow.PermissionRepository.Insert(new Permission
                            {
                                xPermissionGroupID = xNewGroupDB.xID,
                                xController = xGroup.xControllerName,
                                xAction = xItem.xAction,
                                xName = xItem.xName,
                                xActionType = xItem.xActionType,
                                xComment = xItem.xComment
                            });
                        else
                        {
                            if (xPermission.xName != xItem.xName)
                                xPermission.xName = xItem.xName;

                            if (xPermission.xActionType != xItem.xActionType)
                                xPermission.xActionType = xItem.xActionType;

                            if (xPermission.xComment != xItem.xComment)
                                xPermission.xComment = xItem.xComment;

                            if (xPermission.xPermissionGroupID != xNewGroupDB.xID)
                                xPermission.xPermissionGroupID = xNewGroupDB.xID;

                            uow.PermissionRepository.Update(xPermission);
                        }
                    }
                }
                else // Group Exist
                {
                    if (xPermissionGroup.xName != xGroup.xName)
                    {
                        xPermissionGroup.xName = xGroup.xName;
                        uow.PermissionGroupRepository.Update(xPermissionGroup);
                    }

                    foreach (var xItem in xGroup.xPermissions)
                    {
                        var xPermission = xDBPermissions.FirstOrDefault(a => a.xAction == xItem.xAction && a.xController == xGroup.xControllerName);

                        if (xPermission == null)
                            uow.PermissionRepository.Insert(new Permission
                            {
                                xPermissionGroupID = xPermissionGroup.xID,
                                xController = xGroup.xControllerName,
                                xAction = xItem.xAction,
                                xName = xItem.xName,
                                xActionType = xItem.xActionType
                            });
                        else
                        {
                            if (xPermission.xName != xItem.xName)
                                xPermission.xName = xItem.xName;

                            if (xPermission.xActionType != xItem.xActionType)
                                xPermission.xActionType = xItem.xActionType;

                            if (xPermission.xComment != xItem.xComment)
                                xPermission.xComment = xItem.xComment;

                            if (xPermission.xPermissionGroupID != xPermissionGroup.xID)
                                xPermission.xPermissionGroupID = xPermissionGroup.xID;

                            uow.PermissionRepository.Update(xPermission);
                        }
                    }
                }
                await uow.SaveChangesAsync();
            }
            await uow.SaveChangesAsync();
            return true;
        }


        #endregion
    }
}
