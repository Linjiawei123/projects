using MicroService.Dto.OAModels;
using MicroService.Interfaces;
using MicroService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MicroService.Extend
{
    public class UserInfoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public UserInfoMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
                var loginUser = scope.ServiceProvider.GetService<ILoginUserRepository>();

                if (context.User.Identity.IsAuthenticated)
                {
                    var user = await userRepository.GetByAccountAsync(context.User.Identity.Name);
                    if (user != null)
                    {
                        List<UserRights> userRights = await loginUser.GetUserRightsAsync(user.Id);
                        var list = await loginUser.GetUserMenusAsync(user.Id);
                        List<MenuSimple> userMenus = MenusMethod(list);
                        context.Items.Add("User", user);
                        context.Items.Add("UserRights", userRights ?? new List<UserRights>());
                        context.Items.Add("UserMenus", userMenus ?? new List<MenuSimple>());
                    }
                }
            }

            await _next(context);
        }

        private List<MenuSimple> MenusMethod(List<Menu> list)
        {
            List<MenuSimple> mlist = new();
            foreach (var item in list)
            {
                mlist.Add(new MenuSimple()
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Name = item.Name,
                    Code = item.Code,
                    Sort = item.Sort,
                    Url = item.Url,
                    Icon = item.Icon,
                    Remark = item.Remark,
                    Grade = item.Grade,
                    IsDefault = item.IsDefault,
                    IsBlank = item.IsBlank,
                    Child = new List<MenuSimple>()
                });
            }
            List<MenuSimple> topList = mlist.FindAll(w => w.ParentId == Guid.Empty);//最高级菜单
            List<MenuSimple> childList = mlist.FindAll(w => w.ParentId != Guid.Empty);//所有子菜单
            foreach (var item in topList)
            {
                AddChildMenu(item, childList);
            }
            return topList;
        }

        private void AddChildMenu(MenuSimple parent, List<MenuSimple> childList)
        {
            var childs = childList.FindAll(w => w.ParentId == parent.Id);
            if (childs != null && childs.Count > 0)
            {
                foreach (var item in childs)
                {
                    parent.Child.Add(item);
                    AddChildMenu(item, childList);
                }
            }
        }
    }

}
