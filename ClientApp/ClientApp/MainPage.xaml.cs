using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ClientApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //send message to another user 
        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            Dictionary<String, String> formDataDictionary = new Dictionary<String, String>();
            formDataDictionary.Add("SenderUserName", txtSenderUserName.Text);
            formDataDictionary.Add("SenderPhoneNumber", txtSenderPhoneNumber.Text);
            formDataDictionary.Add("ReceiverUserName", txtReceiverUserName.Text);
            formDataDictionary.Add("ReceiverPhoneNumber", txtReceiverPhoneNumber.Text);
            formDataDictionary.Add("MessageData", txtMessageData.Text);
            HttpFormUrlEncodedContent formData = new HttpFormUrlEncodedContent(formDataDictionary);
            var response = await client.PostAsync(new Uri("http://localhost:43492/message/send"), formData);
            Windows.UI.Popups.MessageDialog msg = new Windows.UI.Popups.MessageDialog(await response.Content.ReadAsStringAsync() , "Response Message" );
            msg.ShowAsync();
        }

        //read list of messages sent to the user by other users
        private async void btnRead_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync( new Uri("http://localhost:43492/message/list?RequesterUserName=" + txtSenderUserName.Text));
            //Windows.UI.Popups.MessageDialog msg = new Windows.UI.Popups.MessageDialog(await response.Content.ReadAsStringAsync(), "Response Message");
            //msg.ShowAsync();
            JsonArray JsonResponse = JsonArray.Parse(await response.Content.ReadAsStringAsync());
            foreach (var item in JsonResponse)
            {
                var obj = item.GetObject();
                var message = obj["messageData"].GetString();
                var messageSender = obj["senderUserName"].GetString();
                lstMessages.Items.Add(messageSender + " : " + message);
            }
            
        }
    }
}
