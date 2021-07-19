using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace TestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnSubmit.IsEnabled = false;
                OrderRequest orderRequest = new OrderRequest();
                orderRequest.OrderText = txtOrderText.Text;

                HttpClient client = new HttpClient();
                client.BaseAddress = new System.Uri("http://localhost:5000/orders");
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8,
                                        "application/json");
                var response = client.SendAsync(httpRequestMessage).GetAwaiter().GetResult();
                var stringResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(stringResponse);
                txtResponseText.Text = $"OrderId {orderResponse.OrderId} AgentId {orderResponse.AgentId}";
            }
            catch (System.Exception)
            {
            }
            btnSubmit.IsEnabled = true;
        }
    }
}
