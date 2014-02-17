using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MessageCollection Messages
        {
            get { return (MessageCollection)GetValue(MessagesProperty); }
            set { SetValue(MessagesProperty, value); }
        }
        public static readonly DependencyProperty MessagesProperty =
            DependencyProperty.Register("Messages", typeof(MessageCollection), typeof(MainWindow), new PropertyMetadata(null));

        public MainWindow()
        {
            Messages = new MessageCollection();
            InitializeComponent();
        }

        private static void Animate(IEnumerable<Action> actions, int delay = 200)
        {
            new Thread(() =>
            {
                var rand = new Random();
                foreach (var i in actions)
                {
                    Application.Current.Dispatcher.BeginInvoke(i);
                    Thread.Sleep(delay);
                }
            }).Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = new[] {
                new Message {
                    Author="Lorem Ipsum", Text="Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?" },
                new Message {
                    Author="vero", Text="At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio." },
                new Message {
                    Author="libero", Text="Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat." },
                new Message {
                    Author="Quis", Text="Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?" },
                new Message {
                    Author="Lorem Ipsum", Text="Eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo." },
                new Message {
                    Author="vero", Text="Molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio." }
            };

            Animate(list.Select(i => (Action)(() => activate(i))));
        }

        private void activate(Message i)
        {
            i.Visible = true;
            Messages.Add(i);
            scroller.ScrollToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addMessage();
        }

        private void addMessage()
        {
            if (input.Text.Length == 0)
                return;

            activate(new Message
            {
                Author = "Anonymous",
                Text = input.Text
            });
            input.Text = string.Empty;
        }

        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                addMessage();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Messages.Count > 0)
            {
                var i = Messages.Count - 1;
                Animate(new Action[] {
                    () => Messages[i].Visible = false,
                    () => Messages.RemoveAt(i)
                }, 800);
            }
        }

        private void reply_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var m = (Message)((Image)sender).DataContext;
            var ix = Messages.IndexOf(m);
            var c = Messages.Count - 1;
            Animate(new Action[] {
                () => { for (int i = c; i > ix; i--) Messages[i].Visible = false; },
                () => { for (int i = c; i > ix; i--) Messages.RemoveAt(i); input.Focus(); }
            }, 800);
        }
    }
}
