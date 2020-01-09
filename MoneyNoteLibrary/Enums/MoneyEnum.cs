using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyNoteLibrary.Enums
{
    public class MoneyEnum
    {
        /// <summary>
        /// 수입/지출 구분
        /// </summary>
        public enum MoneyCategory 
        { 
            /// <summary>
            /// 지출
            /// </summary>
            Expense,
            /// <summary>
            /// 수입
            /// </summary>
            Income  
        }
    }
}
