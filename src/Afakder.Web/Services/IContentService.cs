using Afakder.Web.Models.ViewModels;

namespace Afakder.Web.Services;

public interface IContentService
{
    Task<HomePageViewModel> GetHomePageViewModelAsync();
}
