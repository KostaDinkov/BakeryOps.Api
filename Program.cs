using Orders.StartUp;
using FirebirdSql.Data.Client;
using FirebirdSql.Data.FirebirdClient;
using System.Text;

internal class Program
{
    private static  void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);   
        builder.Services.ConfigureServices(builder);
        FbConnection fbConnection = new FbConnection(@"Server=192.168.1.126;Port=3050;User=SYSDBA;Password=masterkey;Database=C:/Gensoft/Sklad.GDB;Charset=WIN1251");
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        fbConnection.Open();
        string sql = "select * from STOKI_DEF\r\n-- where PROPS like '%|6—1|%'\r\nwhere IS_ACTIVE ='Y' And GRUPA not in ('Опаковки', 'Бонбони','Аромати, Овкусители, Подобрители','Основни','Кутии','Готови Смеси и Полуфабрикати','Санитарни Консумативи', 'Фарситури','Кафе','Шоколади',\r\n 'Ядки и Плодове','Консумативи','Заготовки','15 септември / 24 май', 'Баба Марта', 'Великден','Временни','Декорации','Разходи','Суровини','Търговия')\r\norder by Grupa";
        FbCommand fbCommand = new FbCommand(sql, fbConnection);
        FbDataReader reader = fbCommand.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(reader.GetString(2));
        }
        reader.Close();
        fbConnection.Close();

        var app = builder.Build();
        app.ConfigureSwagger();
        app.MapProductEndpoints();
        // Load Products
        app.Run();
    }
}