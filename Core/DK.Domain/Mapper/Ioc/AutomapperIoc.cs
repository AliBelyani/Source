using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

namespace DK.Domain.Mapper.Ioc
{
    public static class AutomapperAddConfigs
    {
        public static void AddMapperConfigurations(this IServiceCollection services)
        {
            services.AddAutoMapper(addAllConfigs);
        }

        private static void addAllConfigs(IMapperConfigurationExpression expression)
        {
            MethodInfo selectMethod = typeof(Enumerable).GetMethods().Where(m => m.Name == "Select")
                .FirstOrDefault(f => f.GetParameters().Any(a => a.Name.Equals("selector")));

            MethodInfo ApplyConfigMethod = typeof(AutomapperAddConfigs).GetMethod("ApplyConfig");
            var x = typeof(AutomapperAddConfigs).GetMethods();

            IEnumerable<Type> xViewModels = typeof(DtoForAttribute).Assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(DtoForAttribute), true).Any());

            foreach (var xModel in xViewModels)
            {
                Type xEntityType = ((DtoForAttribute)xModel.GetCustomAttribute(typeof(DtoForAttribute), true)).EntityClass;
                ApplyConfigMethod.MakeGenericMethod(xEntityType, xModel).Invoke(null, new object[] { expression, selectMethod });
            }
        }

        public static void ApplyConfig<TSource, TDestination>(IMapperConfigurationExpression expression, MethodInfo selectMethod)
        {
            var xCreatedMap = expression.CreateMap<TSource, TDestination>();

            Type xViewModel = typeof(TDestination);
            Type xEntityType = typeof(TSource);

            var xProps = xViewModel.GetProperties();
            foreach (var xProp in xProps)
            {
                var xDesc = (MapFromAttribute[])xProp.GetCustomAttributes(typeof(MapFromAttribute), false);
                if (xDesc.Length != 0)
                {
                    if (xDesc[0].PropertyName.Contains("."))
                    {

                        var xProperySplit = xDesc[0].PropertyName.Split('.');
                        if (xProperySplit.Length > 2)
                            throw new Exception("بیشتر از یک نقطه در مپ فرام امکان پذیر نیست");
                        var xRelationProp = xEntityType.GetProperty(xProperySplit[0]);

                        if (xRelationProp != null)
                        {
                            if (xRelationProp.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(xRelationProp.PropertyType))
                            {
                                ParameterExpression xEntityParameter = Expression.Parameter(xEntityType, "p");
                                Expression xRelationExpression = xEntityParameter;
                                xRelationExpression = Expression.Property(xRelationExpression, xRelationProp); // x => x.relation

                                var xListItemType = xRelationProp.PropertyType.GetGenericArguments()[0];
                                ParameterExpression xSelectParameter = Expression.Parameter(xListItemType);
                                Expression xSelectExpression = xSelectParameter;
                                var xSelectedProperty = xListItemType.GetProperty(xProperySplit[1]);
                                xSelectExpression = Expression.Property(xSelectExpression, xSelectedProperty); // rel => rel.Property
                                var xSelectFunc = typeof(Func<,>).MakeGenericType(xListItemType, xSelectedProperty.PropertyType);

                                var xMapFromExpression = Expression.Call(
                                  null,
                                  selectMethod.MakeGenericMethod(new Type[] { xListItemType, xSelectedProperty.PropertyType }),
                                  new Expression[] { xRelationExpression, Expression.Lambda(xSelectFunc, xSelectExpression, xSelectParameter) }); // x => x.relation.Select(rel => rel.property)

                                var DestinationListType = typeof(IEnumerable<>).MakeGenericType(xSelectedProperty.PropertyType);

                                var xMapFromFunc = typeof(Func<,>).MakeGenericType(xEntityType, DestinationListType);

                                var xLambda = Expression.Lambda(xMapFromFunc, xMapFromExpression, xEntityParameter);

                                xCreatedMap.ForMember(xProp.Name, m => m.DynamicMapFrom(DestinationListType, xLambda));

                            }
                            else if (xRelationProp.PropertyType != typeof(string) && xRelationProp.PropertyType.IsClass)
                            {
                                //expression.CreateMap<User, UserTestVM>().ForMember("xName", m => m.MapFrom(map => map.xPerson.xNationalID));
                                ParameterExpression xEntityParameter = Expression.Parameter(xEntityType, "p");
                                Expression xRelationExpression = xEntityParameter;
                                xRelationExpression = Expression.Property(xRelationExpression, xRelationProp); // x => x.relation
                                var xSelectedProperty = xRelationProp.PropertyType.GetProperty(xProperySplit[1]);
                                var xMapFromExpression = Expression.Property(xRelationExpression, xSelectedProperty);
                                var xMapFromFunc = typeof(Func<,>).MakeGenericType(xEntityType, xSelectedProperty.PropertyType);
                                var xLambda = Expression.Lambda(xMapFromFunc, xMapFromExpression, xEntityParameter);
                                xCreatedMap.ForMember(xProp.Name, m => m.DynamicMapFrom(xSelectedProperty.PropertyType, xLambda));
                            }
                        }
                    }
                    else
                    {
                        xCreatedMap.ForMember(xProp.Name, m => m.MapFrom(xDesc[0].PropertyName));
                    }
                }
            }
            xCreatedMap.ReverseMap();
        }
        public static void DynamicMapFrom<TSource, TDestination, TMember>(this IMemberConfigurationExpression<TSource, TDestination, TMember> xExpression, Type xSelectedPropertyType, LambdaExpression xMapExpression)
        {
            MethodInfo method = xExpression.GetType().GetMethods().Where(m => m.Name == "MapFrom").FirstOrDefault(f => f.GetParameters().Any(a => a.Name.Equals("mapExpression")));
            MethodInfo generic = method.MakeGenericMethod(xSelectedPropertyType);
            generic.Invoke(xExpression, new object[] { xMapExpression });
        }
    }
}
