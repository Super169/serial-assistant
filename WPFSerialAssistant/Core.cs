﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPFSerialAssistant
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 核心初始化
        /// </summary>
        private void InitCore()
        {
            InitClockTimer();
            InitSerialPort();
        }

        #region 状态栏
        /// <summary>
        /// 更新时间信息
        /// </summary>
        private void UpdateTimeDate()
        {
            string timeDateString = "";
            DateTime now = DateTime.Now;
            timeDateString = string.Format("{0}年{1}月{2}日 {3}:{4}:{5}", 
                now.Year, 
                now.Month.ToString("00"), 
                now.Day.ToString("00"), 
                now.Hour.ToString("00"), 
                now.Minute.ToString("00"), 
                now.Second.ToString("00"));

            timeDateTextBlock.Text = timeDateString;
        }

        /// <summary>
        /// 警告信息提示（一直提示）
        /// </summary>
        /// <param name="message">提示信息</param>
        private void Alert(string message)
        {
            // #FF68217A
            statusBar.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x68, 0x21, 0x7A));
            statusInfoTextBlock.Text = message;
        }

        /// <summary>
        /// 普通状态信息提示
        /// </summary>
        /// <param name="message">提示信息</param>
        private void Information(string message)
        {
            if (serialPort.IsOpen)
            {
                // #FFCA5100
                statusBar.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xCA, 0x51, 0x00));
            }
            else
            {
                // #FF007ACC
                statusBar.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x7A, 0xCC));
            }
            statusInfoTextBlock.Text = message;
        }

        #endregion

        private void RecvDataBoxAppend(string textData)
        {
            this.recvDataRichTextBox.AppendText(textData);
            this.recvDataRichTextBox.ScrollToEnd();
        }

        private void SendData()
        {
            string textToSend = sendDataTextBox.Text;
            if (string.IsNullOrEmpty(textToSend))
            {
                Alert("要发送的内容不能为空！");
            }
            // 首先检查自动发送功能是否打开，如果没打开，则发送一次；否则，启动定时器，并且在定时器中断中发送
            if (autoSendEnableCheckBox.IsChecked == true)
            {
                // StartAutoSendDataTimer();
            }
            else
            {
                SerialPortWrite(textToSend);
            }
        }
    }
}
