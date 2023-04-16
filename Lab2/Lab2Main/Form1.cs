using PluginInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Lab2Main
{
    public partial class Form1 : Form
    {
        Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();

        void FindPlugins()
        {
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;

            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginInterface.IPlugin");

                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
            }
        }

        private void OnPluginClick(object sender, EventArgs args)
        {
            IPlugin plugin = plugins[((ToolStripMenuItem)sender).Text];
            pictureBox1.Image = plugin.Transform((Bitmap)pictureBox1.Image);
        }


        void CreatePluginsMenu()
        {
            foreach (string pluginName in plugins.Keys)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(pluginName);
                item.Click += OnPluginClick;
                filtersToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        public Form1()
        {
            InitializeComponent();
            FindPlugins();
            CreatePluginsMenu();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
