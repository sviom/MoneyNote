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
            SignUp,
            /// <summary>
            /// 로그인
            /// </summary>
            LogIn
        }

        /// <summary>
        /// 연결 컨트롤러 이름
        /// </summary>
        public enum ControllerEnum
        {
            /// <summary>
            /// 금액관련
            /// </summary>
            money,
            /// <summary>
            /// 사용자관련
            /// </summary>
            user
        }
    }
}
