﻿using System;
using System.Collections.Generic;
/*	
 */
using System.IO.Ports;															//	使用System.IO.Ports函式庫
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;                                                     //	使用System.Windows.Forms函式庫

namespace UartOscilloscope                                                      //	命名空間為UartOscilloscope
{                                                                               //	進入命名空間
	public class UARTConnection                                                 //	UARTConnection類別
	{                                                                           //	進入UARTConnection類別
		/// <summary>
		/// 宣告靜態常數
		/// </summary>
		private const int DefaultBaudRate = 9600;                               //	宣告DefaultBaudRate(預設鮑率)常數
		/*	同位位元設定說明：0為不檢查(None),1為奇同位檢察,2為偶同位檢察,3為同位位元恆為1,4為同位位元恆為0 */
		private const Parity DefaultParitySetting = 0;                          //	宣告DefaultParitySetting(預設同位位元設定)常數
		
		public static SerialPort Uart_comport;                                  //	宣告新的SerialPort通訊埠，名稱為Uart_comport
		private static int BaudRate;                                            //	宣告BaudRate靜態私有變數，控制SerialPort連線鮑率
		private static Parity ParitySetting;                                    //	宣告ParitySetting靜態私有變數，控制SerialPort串列埠之Parity同位位元設定
		private static int DataBitsSetting;                                     //	宣告DataBitsSetting靜態私有變數，控制SerialPort串列埠之DataBits數值
		private static int ConnectedCOMPortNum;                                 //	宣告ConnectedCOMPortNum私有靜態變數，記錄已連接的SerialPort數量
		private static bool Uart_comport_connected;                             //	宣告Uart_comport_connected布林變數，表示Uart_comport連線狀態
		public UARTConnection()                                                 //	UARTConnection建構子
		{                                                                       //	進入UARTConnection建構子

		}                                                                       //	結束UARTConnection建構子
		public static void InitializeUARTConnectionSetting()                    //	InitializeUARTConnectionSetting方法，初始化UART連線參數
		{                                                                       //	進入InitializeUARTConnectionSetting方法
			BaudRate = DefaultBaudRate;                                         //	預設BaudRate數值為DefaultBaudRate
			ParitySetting = DefaultParitySetting;                               //	預設ParitySetting數值為0(無同位位元檢查)
			DataBitsSetting = 8;                                                //	預設DataBitsSetting數值為8
			ConnectedCOMPortNum = 0;                                            //	預設ConnectedCOMPortNum為0
			Uart_comport_connected = false;                                     //	預設Uart_comport_connected值為False
		}                                                                       //	結束InitializeUARTConnectionSetting方法
		public static int Get_BaudRate()                                        //	Get_BaudRate方法
		{                                                                       //	進入Get_BaudRate方法
			return BaudRate;                                                    //	回傳BaudRate數值
		}                                                                       //	結束Get_BaudRate方法
		public static void Set_BaudRate(int NewBaudRate)                        //	Set_BaudRate方法
		{                                                                       //	進入Set_BaudRate方法
			BaudRate = NewBaudRate;                                             //	設定BaudRate
		}                                                                       //	進入Set_BaudRate方法
		public static Parity Get_ParitySetting()                                //	Get_ParitySetting方法
		{                                                                       //	進入Get_ParitySetting方法
			return ParitySetting;                                               //	回傳ParitySetting數值
		}                                                                       //	結束Get_ParitySetting方法
		public static void Set_ParitySetting(Parity NewParitySetting)           //	Set_ParitySetting方法
		{                                                                       //	進入Set_ParitySetting方法
			ParitySetting = NewParitySetting;                                   //	設定ParitySetting
		}                                                                       //	結束Set_ParitySetting方法
		public static int Get_DataBitsSetting()                                 //	Get_DataBitsSetting方法
		{                                                                       //	進入Get_DataBitsSetting方法
			return DataBitsSetting;                                             //	回傳DataBitsSetting數值
		}                                                                       //	結束Get_DataBitsSetting方法
		public static void Set_DataBitsSetting(int NewDataBitsSetting)          //	Set_DataBitsSetting方法
		{                                                                       //	進入Set_DataBitsSetting方法
			DataBitsSetting = NewDataBitsSetting;                               //	設定DataBitsSetting數值
		}                                                                       //	結束Set_DataBitsSetting方法
		public static int Get_ConnectedCOMPortNum()                             //	Get_ConnectedCOMPortNum方法
		{                                                                       //	進入Get_ConnectedCOMPortNum方法
			return ConnectedCOMPortNum;                                         //	回傳ConnectedCOMPortNum數值
		}                                                                       //	結束Get_ConnectedCOMPortNum方法
		public static void Set_ConnectedCOMPortNum(int NewConnectedCOMPortNum)  //	Set_ConnectedCOMPortNum方法
		{                                                                       //	進入Set_ConnectedCOMPortNum方法
			ConnectedCOMPortNum = NewConnectedCOMPortNum;                       //	設定ConnectedCOMPortNum數值
		}                                                                       //	結束Set_ConnectedCOMPortNum方法
		public static void Set_Uart_comport_connected(bool NewUart_comport_connected)
		//	Set_Uart_comport_connected方法
		{                                                                       //	進入Set_Uart_comport_connected方法
			Uart_comport_connected = NewUart_comport_connected;                 //	設定Uart_comport_connected
		}                                                                       //	結束Set_Uart_comport_connected方法
		public static bool Get_Uart_comport_connected()                         //	Get_Uart_comport_connected方法，用以取得Uart_comport_connected狀態
		{                                                                       //	進入Get_Uart_comport_connected方法
			return Uart_comport_connected;                                      //	回傳Uart_comport_connected狀態
		}                                                                       //	結束Get_Uart_comport_connected方法
		public void list_SerialPort()                                           //	偵測並列出已連線SerialPort副程式
		{                                                                       //	進入list_SerialPort副程式
			DebugVariables.Set_list_SerialPort_Runtimes();                      //	呼叫Set_list_SerialPort_Runtimes方法遞增list_SerialPort_Runtimes變數
			string[] ports = SerialPort.GetPortNames();                         //	偵測已連線的SerialPort並儲存結果至陣列ports
			Form1.comboBox1ItemClear();
			if (ports.Length == 0)                                              //	若偵測不到任何已連接的SerialPort(ports.Length為0)
			{                                                                   //	進入if敘述
				ErrorCodeMessage.Error_Message_Show((int)ErrorCodeMessage.ErrorCodeEncoding.NoSerialPortConnected);
				//	顯示錯誤訊息
				button2.Enabled = false;                                        //	關閉"連線/中斷連線"按鈕功能
				textBox1.Enabled = false;                                       //	關閉textBox1(接收字串資料文字方塊)功能
				return;                                                         //	提早結束list_SerialPort副程式
			}                                                                   //	結束if敘述
			else                                                                //	若偵測到已連線的SerialPort
			{                                                                   //	進入else敘述
				UARTConnection.Set_ConnectedCOMPortNum(ports.Length);           //	記錄已連線的SerialPort數量
				foreach (string port in ports)                                  //	依序處理每個已連線的SerialPort
				{                                                               //	進入foreach敘述
					comboBox1.Items.Add(port);                                  //	以條列式選單(comboBox1)列出已連線的SerialPort
				}                                                               //	結束foreach敘述
				button2.Enabled = false;                                        //	暫時關閉"連線"按鈕功能，待使用者選定愈連線之Serialport(未選定連線Serialport，可避免發生Error_010002)
				textBox1.Enabled = true;                                        //	開啟textBox1(接收字串資料文字方塊)功能
				return;                                                         //	結束list_SerialPort副程式
			}                                                                   //	結束else敘述
		}                                                                       //	結束list_SerialPort副程式
		/// <summary>
		/// Uart_comport_handle副程式
		/// Uart_comport_handle副程式用於處理Uart_comport連線設定
		///  呼叫格式為Uart_comport_handle(comport名稱)
		///  
		/// </summary>
		/// <param name="comport_name"></param>
		public void Uart_comport_handle(string comport_name)                    //	串列埠連線處理Uart_comport_handle副程式
		{                                                                       //	進入Uart_comport_handle副程式
			DebugVariables.Set_Uart_comport_handle_Runtimes();                  //	呼叫Set_Uart_comport_handle_Runtimes方法遞增Uart_comport_handle_Runtimes變數
			if (UARTConnection.Get_Uart_comport_connected() == true)            //  若Uart_comport_connected為True，代表Uart_comport連線中，將執行中斷連線
			{                                                                   //	進入if敘述
				label6.Text = (comport_name + "正在中斷連線");
				//  顯示連線狀態為(comport_name + "正在中斷連線")，如"COM1正在中斷連線"
				UARTConnection.Set_Uart_comport_connected(false);               //	更新Uart_comport_connected
				UARTConnection.Uart_comport.Close();                            //	關閉Uart_comport連線
				button2.Text = "連線";                                          //	更改button2文字為"連線"
				button2.Enabled = true;                                         //	重新開啟"連線/中斷連線"按鈕功能
				label6.Text = "未連線";                                         //	顯示連線狀態為"未連線"
				return;                                                         //	結束Uart_comport_handle副程式
			}                                                                   //	結束if敘述
			else                                                                //	若Uart_comport_connected為False，執行連線
			{                                                                   //	進入else敘述
				label6.Text = "偵測連接埠設定";                                 //	顯示連線狀態為"偵測連接埠設定"
				if (comport_name == "")                                         //	若comport_name為空白(Combobox1未選定)
				{                                                               //	進入if敘述
					ErrorCodeMessage.Error_Message_Show((int)ErrorCodeMessage.ErrorCodeEncoding.NoSerialPortSelected);
					//	顯示錯誤訊息
					button2.Enabled = true;                                     //	重新開啟"連線/中斷連線"按鈕功能
					return;                                                     //	結束Uart_comport_handle副程式
				}                                                               //	結束if敘述
				else                                                            //	已選定連接埠
				{                                                               //	進入else敘述
					label6.Text = (comport_name + "正在嘗試連線");              //	顯示連線狀態為(comport_name + "正在嘗試連線")，如"COM1正在嘗試連線"
					try                                                         //	嘗試以comport_name建立串列通訊連線
					{                                                           //	進入try敘述
						UARTConnection.Uart_comport = new SerialPort(comport_name);
						//	Uart_comport串列埠建立comport_name連線
					}                                                           //	結束try敘述
					catch (System.IO.IOException)                               //	當IO發生錯誤時的例外狀況
					{                                                           //	進入catch敘述
						ErrorCodeMessage.Error_Message_Show((int)ErrorCodeMessage.ErrorCodeEncoding.SerialPortConnectError);
						//	顯示錯誤訊息
						button2.Enabled = true;                                 //	重新開啟"連線/中斷連線"按鈕功能
						return;                                                 //	結束Uart_comport_handle副程式
					}                                                           //	結束catch敘述
					UARTConnection.Uart_comport.BaudRate = UARTConnection.Get_BaudRate();
					//	設定Uart_comport連線之BaudRate
					UARTConnection.Uart_comport.DataBits = UARTConnection.Get_DataBitsSetting();
					//	設定Uart_comport連線之DataBits
					switch (UARTConnection.Get_ParitySetting())                 //	根據UARTConnection.ParitySetting變數進行同位位元設定
					{                                                           //	進入switch-case敘述
						case 0:                                                 //	若為case0(UARTConnection.ParitySetting為0)
							UARTConnection.Uart_comport.Parity = Parity.None;   //	無同位元檢查
							break;                                              //	結束case0敘述、跳出switch-case
						case 1:                                                 //	若為case1(UARTConnection.ParitySetting為1)
							UARTConnection.Uart_comport.Parity = Parity.Odd;    //	奇同位位元檢查
							break;                                              //	結束case1敘述、跳出switch-case
						case 2:                                                 //	若為case2(UARTConnection.ParitySetting為2)
							UARTConnection.Uart_comport.Parity = Parity.Even;   //	偶同位位元檢查
							break;                                              //	結束case2敘述、跳出switch-case
						case 3:                                                 //	若為case3(UARTConnection.ParitySetting為3)
							UARTConnection.Uart_comport.Parity = Parity.Mark;   //	將同位位元恆設為1
							break;                                              //	結束case3敘述、跳出switch-case
						case 4:                                                 //	若為case4(UARTConnection.ParitySetting為4)
							UARTConnection.Uart_comport.Parity = Parity.Space;  //	將同位位元恆設為0
							break;                                              //	結束case4敘述、跳出switch-case
					}                                                           //	結束switch-case敘述
					try                                                         //	以try方式執行資料接收
					{                                                           //	進入try敘述
						if (!UARTConnection.Uart_comport.IsOpen)                //	若Uart_comport未開啟
						{                                                       //	進入if敘述
							UARTConnection.Uart_comport.Open();                 //	開啟Uart_comport
							label6.Text = (comport_name + "已連線");            //	顯示連線狀態為(comport_name + "已連線")，如"COM1已連線"
							UARTConnection.Set_Uart_comport_connected(true);    //	更新Uart_comport_connected狀態
							button2.Text = "中斷連線";                          //	更改button2文字為"中斷連線"
							button2.Enabled = true;                             //	重新開啟"連線/中斷連線"按鈕功能
						}                                                       //	結束if敘述
						UARTConnection.Uart_comport.DataReceived += new SerialDataReceivedEventHandler(comport_DataReceived);
						//  處理資料接收
					}                                                           //  結束try敘述
					catch (Exception ex)                                        //  若發生錯誤狀況
					{                                                           //  進入catch敘述
						var result = MessageBox.Show                            //  顯示錯誤訊息
							(                                                   //  進入錯誤訊息MessageBox設定
								ex.ToString(), "DataReceived Error",            //  顯示錯誤訊息ex.ToString()，標題為"DataReceived Error"
								MessageBoxButtons.OK,                           //  MessageBox選項為OK
								MessageBoxIcon.Error                            //  顯示錯誤標誌
							);                                                  //  結束錯誤訊息MessageBox設定
						UARTConnection.Uart_comport.Close();                    //  關閉Uart_comport連線
						button2.Text = "連線";                                  //  更改button2文字為"連線"
						button2.Enabled = true;                                 //  重新開啟"連線/中斷連線"按鈕功能
						return;                                                 //  結束Uart_comport_handle副程式
					}                                                           //  結束catch敘述
				}                                                               //  結束else敘述
			}                                                                   //  結束else敘述
		}                                                                       //  結束Uart_comport_handle副程式  
	}                                                                           //	結束UARTConnection類別
}																				//	結束命名空間
