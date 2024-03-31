using Client.Enums;
using Microsoft.AspNetCore.Components;

namespace Client.Layout.Navigation;

public partial class SearchForm
{
	[Inject] NavigationManager? NavigationManager { get; set; }

	protected Nav? CurrentNav { get; set; }

	private async Task<IEnumerable<Nav>> SearchNavs(string searchText)
	{
		return await Task.FromResult(Nav.List
			.Where(x => x.Title.ToLower().Contains(searchText.ToLower()) 
				&& x.IsPartOfList(PageListType.SitemapPage))
			.OrderBy(o => o.Name));
	}

	private void SelectedResultChanged(Nav nav)
	{
		NavigationManager!.NavigateTo(nav!.Index);
	}

}