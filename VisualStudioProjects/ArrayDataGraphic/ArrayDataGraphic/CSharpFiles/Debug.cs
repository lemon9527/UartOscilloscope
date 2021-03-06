﻿/********************************************************************
 * Develop by Jimmy Hu												*
 * This program is licensed under the Apache License 2.0.			*
 * Debug.cs															*
 * 本檔案用於宣告偵錯相關工具物件									*
 ********************************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayDataGraphic.CSharpFiles
{                                                                               //	namespace start, 進入命名空間
	class Debug                                                                 //	Debug class, Debug類別
	{                                                                           //	Debug class start, 進入Debug類別
		public readonly static Debug Instance = new Debug();                    //	Debug class instance, 設計Debug存取介面

		/// <summary>
		/// DebugMode would be setted "true" during debugging.
		/// 在偵錯模式中將DebugMode設為true
		/// </summary>
		public readonly static bool DebugMode = true;                           //	DebugMode variable

		/// <summary>
		/// ShowQueueData method could print the data in InputQueue.
		/// ShowQueueData用於顯示輸入Queue的資料
		/// </summary>
		/// <param name="InputQueue">欲顯示內容之Queue</param>
		public void ShowQueueData(Queue<object> InputQueue)                     //	ShowQueueData method, ShowQueueData方法
		{                                                                       //	ShowQueueData method start, 進入ShowQueueData方法
			foreach (var item in InputQueue)                                    //	get each element in InputQueue, 取得InputQueue各項元素
			{																	//	foreach statement start, 進入foreach敘述
				Console.WriteLine(item.ToString());                             //	show element data, 顯示元素資料
			}																	//	foreach statement end, 結束foreach敘述
		}                                                                       //	ShowQueueData method end, 結束ShowQueueData方法
	}                                                                           //	Debug class eud, 結束Debug類別
}                                                                               //	namespace end, 結束命名空間
