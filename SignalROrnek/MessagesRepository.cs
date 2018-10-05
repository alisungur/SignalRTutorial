using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SignalROrnek
{
    public class MessagesRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["SignalRConnection"].ConnectionString;

        public IEnumerable<SignalRVeri> GetAllMessages()
        {
            var messages = new List<SignalRVeri>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [Id], [Mesaj] FROM [dbo].[SignalRVeris]", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();
                    //Mesajlarımızı okuyoruz. Aslında burada klasik Db işlemi yapıyoruz.
                    while (reader.Read())
                    {
                        messages.Add(item: new SignalRVeri { Id = (int)reader["Id"], Mesaj = (string)reader["Mesaj"] });
                    }
                }

            }
            return messages;


        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                BildirimHub.SendMessages();
            }
        }


    }
}