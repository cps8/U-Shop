using UShop.Services.User.Application.Menu.Vo;
using UShop.Services.User.Application.System.Vo;
using UShop.Services.User.Domain.Repositories;
using UShop.Shared.Dto;
using UShop.Shared.Ioc.ServiceProviderFactorySupport;

namespace UShop.Services.User.Application.Menu
{
    [Service]
    public class MenuService: IMenuServices
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<ResultModel<List<MenuManagerVo>>> GetAll()
        {
            var menus = await _menuRepository.GetAll();
            
            return ResultModel<List<MenuManagerVo>>.Success(menus.Select(m => new MenuManagerVo(m)).ToList());
        }
    }
}
