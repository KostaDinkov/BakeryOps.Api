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

            // generate GUIDs for products with null WEB_PRODUCT_NOTE_EN
            string createGuidsSql = "UPDATE STOKI_DEF\r\n"+
                                    "SET WEB_PRODUCT_NOTE_EN = UUID_TO_CHAR (gen_uuid())\r\n"+
                                    "WHERE WEB_PRODUCT_NOTE_EN is NULL;";
            FbCommand createGuidsCommand = new FbCommand(createGuidsSql,fbConnection);
            createGuidsCommand.ExecuteNonQuery();

            string sql = "select GRUPA, Stoka, Code, COALESCE (CENA_PROD_DR1,0) as CENA_PROD_DR1, COALESCE(CENA_PROD_ED1,0) as CENA_PROD_ED1, DATE_CREATED, WEB_PRODUCT_NOTE_EN, Razfas1 from STOKI_DEF\r\n" +
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
                update.DateCreated = reader.GetDateTime(5);
                update.Id = Guid.Parse(reader.GetString(6));
                update.Unit = reader.GetString(7).Trim();

                
                var existing = db.Products.Find(update.Id);
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
                    existing.DateCreated = update.DateCreated;
                    
                    existing.Unit = update.Unit; 
                    //await db.Products.AddAsync(existing);
                }
            }

            await db.SaveChangesAsync();
            reader.Close();
            fbConnection.Close();
        }
    }
}
