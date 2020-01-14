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

    public static class MoneyApiInfo
    {
        /// <summary>
        /// 서비스와 연결하는 API 종류
        /// </summary>
        public enum MoneyApi
        {
            /// <summary>
            /// 금액 리스트 전부 불러오기
            /// </summary>
            GetAllMoney,
            /// <summary>
            /// 저장
            /// </summary>
            SaveMoney,
            /// <summary>
            /// 업데이트
            /// </summary>
            UpdateMoney,
            /// <summary>
            /// 회원가입
            /// </summary>
            SignUp
        }
    }
}
