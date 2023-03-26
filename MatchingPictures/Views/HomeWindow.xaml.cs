using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Timers;
using System.Media;

namespace MatchingPictures.Views
{
   


  
    public static class ExtensionMethods
    {
        private static Action EmptyDelegate = delegate () { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }

   
    static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
    
    public partial class HomeWindow : Window
    {
        public static string message = "";
        Button b_pre = null;
        Button b_next = null;
        int count = 0;  
        int steps = 50;

        
        List<Image> container = new List<Image>();
        List<Button> buttons = new List<Button>();

        int[] arr = new int[16] { 1, 2, 3, 4, 5, 6, 7, 8, 8, 7, 6, 5, 4, 3, 2, 1,}; 


       public HomeWindow()
        {
            InitializeComponent();
            Initialize_list();
            Randomize_list();
            btn_score.Content = steps.ToString();

        } 

        
        private void Initialize_list()
        {
            container.Add(A1);
            container.Add(A2);
            container.Add(A3);
            container.Add(A4);
            container.Add(A5);
            container.Add(A6);
            container.Add(A7);
            container.Add(A8);
            container.Add(A9);
            container.Add(A10);
            container.Add(A11);
            container.Add(A12);
            container.Add(A13);
            container.Add(A14);
            container.Add(A15);
            container.Add(A16);

            buttons.Add(B1);
            buttons.Add(B2);
            buttons.Add(B3);
            buttons.Add(B4);
            buttons.Add(B5);
            buttons.Add(B6);
            buttons.Add(B7);
            buttons.Add(B8);
            buttons.Add(B9);
            buttons.Add(B10);
            buttons.Add(B11);
            buttons.Add(B12);
            buttons.Add(B13);
            buttons.Add(B14);
            buttons.Add(B15);
            buttons.Add(B16);
        }

        
        private void Randomize_list()
        {
            new Random().Shuffle(arr);
            int rand_num = 0;
            int i = 0;
            foreach (Image img in container)
            {
                rand_num = arr[i];
                img.Source = new BitmapImage(new Uri("/Assets/Pics/P" + rand_num + ".png", UriKind.Relative ));
                i++;
            }
        }

        
        public void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            steps--;
            btn_score.Content = steps.ToString();  

            if (count == 0)
            {
                b_pre = b;
                b_pre.Visibility = Visibility.Hidden;
            }
            else if (count == 1)
            {
                b_next = b;
                b_next.Visibility = Visibility.Hidden;
                b_next.Refresh();

                System.Threading.Thread.Sleep(500);

                if (Check_image())
                {
                    b_pre.IsEnabled = false;
                    b_next.IsEnabled = false;
                    
                }
                else
                {
                    b_pre.Visibility = Visibility.Visible;
                    b_next.Visibility = Visibility.Visible;
                }

                if (Game_over())
                {
                    System.Threading.Thread.Sleep(500);
                    message = "Congratulation, you won";

                }

                else if (steps <= 0)
                {
                    btn_score.Refresh();

                    b_pre.IsEnabled = false;
                    b_next.IsEnabled = false;

                    System.Threading.Thread.Sleep(1000);
                    message = "Sorry, out of moves";

                }
                count = -1;
            }
            count++;
        }

        //Check if currently visible images are same or not
        private bool Check_image()
        {
            string image1 = ImageDictionary(b_pre.Name);
            string image2 = ImageDictionary(b_next.Name);
            string src1 = "";
            string src2 = "";

            foreach (Image img in container)
            {
                if (img.Name == image1 || img.Name == image2)
                {
                    if (src1 == "")

                        src1 = img.Source.ToString();
                    else 
                        src2 = img.Source.ToString();
                    
                }
                if (!src1.Equals("") && !src2.Equals(""))
                                              break;
            }
            

            if (src1 == src2)
                return true;


            return false;
        }

        //Dictionary contain image in their corresponding button
        private string ImageDictionary(string btnName)
        {
            //Dictionary contain key values

            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                { "B1", "A1" },
                { "B2", "A2" },
                { "B3", "A3" },
                { "B4", "A4" },
                { "B5", "A5" },
                { "B6", "A6" },
                { "B7", "A7" },
                { "B8", "A8" },
                { "B9", "A9" },
                { "B10", "A10" },
                { "B11", "A11" },
                { "B12", "A12" },
                { "B13", "A13" },
                { "B14", "A14" },
                { "B15", "A15" },
                { "B16", "A16" }
            };

            if (dictionary.ContainsKey(btnName))
            {
                string value = dictionary[btnName];
                return value;
            }

            return "";
        }

        
        private bool Game_over()
        {
            foreach (Button btn in buttons)
            {
                if (btn.IsEnabled == true)
                    return false;
            }

            return true;
        }



    }
}
