using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 加密程序
{
    public partial class Form1 : Form
    {
        string filePath;
        string fileName;
        int fileCount;

        string[] filePaths;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            /*
             int password;
try
{
    password = int.Parse(textBox1.Text);
    // 如果textBox1.Text能够成功解析为整数，则password的值为解析后的整数值
    // 在这里添加对password变量的操作
}
catch (FormatException)
{
    // 如果textBox1.Text不能够成功解析为整数，则提示用户输入正确的密码格式
}
            */
            //判断 label 的 Text 属性是否为空
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                // 执行操作
                MessageBox.Show("喂，密钥忘了输了你，你开玩笑呢。");
                return;
            }

            string currentDirectory = Directory.GetCurrentDirectory();// 获取当前程序所在目录
            int password = int.Parse(textBox1.Text);
            try
                {
                    // 读取文件内容到字节数组中
                    byte[] buffer = File.ReadAllBytes(filePath);

                    // 如果文件大小超过2GB，则不进行加密处理
                    if (buffer.Length > (2L * 1024 * 1024 * 1024 - 2))
                    {
                        label4.Text = "文件超过2GB，太大了，吃不消，建议发给我我帮你加密。";
                        return;
                    }

                    // 删除原始文件
                    //File.Delete(file);

                    // 遍历字节数组中的每个字节，对其进行加密操作
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = (byte)(buffer[i] ^ password); // 将字节与数字进行异或运算
                    }

                    // 将加密后的字节数组写回到原始文件中
                    File.WriteAllBytes(System.IO.Path.Combine(currentDirectory,fileName), buffer);

                // 设置输出文件路径
                string outputPath = "output.txt";

                // 获取当前时间
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // 创建并打开输出文件
                using (StreamWriter writer = new StreamWriter(outputPath,true))//在输出文件时，如果不想每次覆盖文件，可以使用 StreamWriter 构造函数的第二个参数来指定是否追加到文件中。例如，要将内容追加到现有文件中，请将 StreamWriter 构造函数的第二个参数设置为 true：
                {
                    // 写入解密密钥内容
                    writer.WriteLine();
                    writer.Write("选择了1个文件");
                    writer.Write(currentTime + "    " + password + "    " + filePath);
                }

                label2.Text = "已输出到程序目录文件下";
                }
                catch (Exception ex)
                {
                // 处理异常
                label2.Text = "失败";
                MessageBox.Show($"文件 {filePath} 加密失败，错误信息：{ex.Message}");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button5.Visible = false;
            // 弹出文件选择对话框
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "所有秘密（期待|*.*";
            dialog.Multiselect = false;
            dialog.Title = "选择你的小秘密（嘿嘿";
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            // 如果用户选择了文件，则输出文件的路径、名称和大小
            /*
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = dialog.FileName;
                string fileName = Path.GetFileName(filePath);
                long fileSize = new FileInfo(filePath).Length;
                Console.WriteLine("文件路径：" + filePath);
                Console.WriteLine("文件名称：" + fileName);
                Console.WriteLine("文件大小：" + fileSize + "字节");
            }
            else
            {
                Console.WriteLine("用户未选择文件。");
            }
            */

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                filePath = dialog.FileName;
                fileName = Path.GetFileName(filePath);
                label4.Text = "已选择"+fileName;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // 如果按下的键不是数字、删除键或Backspace键，则将事件标记为已处理
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                label5.Text = "只能是整数";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button5.Visible = true;
            // 弹出文件选择对话框
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "所有秘密（期待|*.*";
            dialog.Multiselect = true;
            dialog.Title = "选择你的小秘密（嘿嘿";
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                filePaths = dialog.FileNames;
                fileCount = dialog.FileNames.Length;
                label4.Text = "已选择" + fileCount+"个文件";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //判断 label 的 Text 属性是否为空
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                // 执行操作
                MessageBox.Show("喂，密钥忘了输了你，你开玩笑呢。");
                return;
            }

            string currentDirectory = Directory.GetCurrentDirectory();// 获取当前程序所在目录
            int password = int.Parse(textBox1.Text);
            // 遍历每个文件，对其进行加密操作
            foreach (string filePath in filePaths)
            {
                try
                {
                    fileName = Path.GetFileName(filePath);
                    // 读取文件内容到字节数组中
                    byte[] buffer = File.ReadAllBytes(filePath);

                    // 如果文件大小超过2GB，则不进行加密处理
                    if (buffer.Length > (2L * 1024 * 1024 * 1024 - 2))
                    {
                        MessageBox.Show("文件超过2GB，太大了，吃不消，建议发给我我帮你加密。");
                        continue;
                    }

                    // 遍历字节数组中的每个字节，对其进行加密操作
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = (byte)(buffer[i] ^ password); // 将字节与数字进行异或运算
                    }

                    // 将加密后的字节数组写回到原始文件中
                    File.WriteAllBytes(System.IO.Path.Combine(currentDirectory, fileName), buffer);

                    // 设置输出文件路径
                    string outputPath = "output.txt";

                    // 获取当前时间
                    string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    // 创建并打开输出文件
                    using (StreamWriter writer = new StreamWriter(outputPath, true))
                    {
                        // 写入解密密钥内容
                        writer.WriteLine();
                        writer.Write("选择了"+fileCount+"个文件");
                        writer.Write(currentTime + "    " + password + "    " + filePath);
                    }

                    //MessageBox.Show($"文件 {filePath} 加密成功！");
                    label2.Text = filePath+"已输出到程序目录文件下";
                }
                catch (Exception ex)
                {
                    // 处理异常
                    MessageBox.Show($"文件 {filePath} 加密失败，错误信息：{ex.Message}");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity >= 0.025)
            {
                this.Opacity -= 0.025;
            }
            else
            {
                timer1.Stop();
                this.Close();
            }
        }
    }
}
