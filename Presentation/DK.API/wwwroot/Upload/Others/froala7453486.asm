کنترلر Froala همش
Option های Froalam در خبر ها ثبت خبر تغییر کرد
در Style.scss باید فونت عوض شود
فونت فارسی در angula.json برای فارسی کردن Froala
خط دوم در کدهای زیر اضافه شود

    app.UseIdentityServer();
            app.UseHttpContext();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });