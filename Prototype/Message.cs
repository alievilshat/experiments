using System.Collections.ObjectModel;
using System.Windows;

namespace Prototype
{
    public class Message : DependencyObject
    {
        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }
        public static readonly DependencyProperty AuthorProperty =
            DependencyProperty.Register("Author", typeof(string), typeof(Message), new PropertyMetadata("Anonymous"));


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Message), new PropertyMetadata(string.Empty));


        public MessageCollection Messages
        {
            get { return (MessageCollection)GetValue(MessagesProperty); }
            set { SetValue(MessagesProperty, value); }
        }
        public static readonly DependencyProperty MessagesProperty =
            DependencyProperty.Register("Messages", typeof(MessageCollection), typeof(Message), new PropertyMetadata(null));


        public bool Visible 
        {
            get { return GetVisible(this); }
            set { SetVisible(this, value); }
        }
        public static bool GetVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(VisibleProperty);
        }
        public static void SetVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(VisibleProperty, value);
        }
        public static readonly DependencyProperty VisibleProperty =
            DependencyProperty.RegisterAttached("Visible", typeof(bool), typeof(Message), new PropertyMetadata(false));


        public Message()
        {
            Messages = new MessageCollection();
        }

        public override string ToString()
        {
            return string.Format("{0}\n{1}", Author, Text);
        }
    }

    public class MessageCollection : ObservableCollection<Message>
    {
    }
}
