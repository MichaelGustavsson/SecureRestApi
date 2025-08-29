using productsApi.Models;

namespace productsApi.Data;

public class InitializeDb
{
    public static async Task SeedData(AppDbContext context)
    {
        if (context.Products.Any()) return;

        var products = new List<Product>
        {
            new(){
                Brand="Bosch",
                Category="Skruvdragare",
                ItemNumber="SKD-1000",
                Name="Bosch GSR 12V-15",
                Description="Borrskruvdragare på 12V med ett varvtal på upp till 1300 varv/min 30 Nm vridmoment. Dess kompakta design ger optimal hantering och gör den perfekt för arbete över huvudhöjd samt i trånga utrymmen.",
                Price=1488
            },
            new(){
                Brand="Milwaukee",
                Category="Skruvdragare",
                ItemNumber="SKD-2000",
                Name="Milwaukee M18 FDD3-502X",
                Description="Milwaukee 4933479863 M18 FUEL kompakt borrskruvdragare med kolborstfri POWERSTATE-motor. Vridmoment på 158 Nm.",
                Price=4699
            },
            new(){
                Brand="Dewault",
                Category="Skruvdragare",
                ItemNumber="SKD-3000",
                Name="Dewalt DCD791D2",
                Description="Borrskruvdragare på 18V med kolborstfri motor som klarar träborrning upp till 40mm. Vridmoment på 70Nm och upp till 2000 varv/min på andra växeln. ",
                Price=2739
            },
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}
