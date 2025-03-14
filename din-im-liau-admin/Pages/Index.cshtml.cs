using Microsoft.AspNetCore.Mvc.RazorPages;

namespace din_im_liau_admin.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    { }
}