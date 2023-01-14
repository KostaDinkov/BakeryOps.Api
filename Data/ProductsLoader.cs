using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Client;
using System.Text;
using Orders.Models;

namespace Orders.Data
{
    public static class ProductsLoader
    {
        public static async Task SyncProductsData(OrdersDB db, string serverAddress, string dbPath)
        {
            FbConnection fbConnection = new FbConnection($"Server={serverAddress};Port=3050;User=SYSDBA;Password=masterkey;Database={dbPath};Charset=WIN1251");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            fbConnection.Open();
            string sql = "select GRUPA, Stoka, Code, COALESCE (CENA_PROD_DR1,0) as CENA_PROD_DR1, COALESCE(CENA_PROD_ED1,0) as CENA_PROD_ED1 from STOKI_DEF\r\n"+
                        "where IS_ACTIVE ='Y'\r\n"+
                        "And GRUPA not in ('Опаковки', 'Бонбони','Аромати, Овкусители, Подобрители','Основни','Кутии','Готови Смеси и Полуфабрикати','Санитарни Консумативи', 'Фарситури','Кафе','Шоколади',\r\n 'Ядки и Плодове','Консумативи','Заготовки','15 септември / 24 май', 'Баба Марта', 'Великден','Временни','Декорации','Разходи','Суровини','Търговия')\r\n"+
                        "order by Grupa";
            FbCommand fbCommand = new FbCommand(sql, fbConnection);
            FbDataReader reader = fbCommand.ExecuteReader();
            while (reader.Read())
            {
                Product update = new Product();
                update.Category = reader.GetString(0).Trim();
                update.Name = reader.GetString(1).Trim();
                update.Code = reader.GetString(2).Trim();
                update.PriceDrebno = (decimal)reader.GetDouble(3);
                update.PriceEdro = (decimal)reader.GetDouble(4);


                var existing = db.Products.FirstOrDefault(p => p.Name == update.Name && p.Code == update.Code);
                if (existing is null)
                {
                    await db.Products.AddAsync(update);
                }
                else
                {
                    existing.Category = update.Category;
                    existing.Name = update.Name;
                    existing.Code = update.Code;
                    existing.PriceDrebno = update.PriceDrebno;
                    existing.PriceEdro = update.PriceEdro;
                    await db.Products.AddAsync(existing);
                }
            }

            await db.SaveChangesAsync();
            reader.Close();
            fbConnection.Close();
        }
    }
}
