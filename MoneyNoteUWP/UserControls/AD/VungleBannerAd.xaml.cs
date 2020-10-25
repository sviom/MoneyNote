﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using VungleSDK;

// 사용자 정의 컨트롤 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234236에 나와 있습니다.

namespace MoneyNote.UserControls.AD
{
    public sealed partial class VungleBannerAd : UserControl
    {
        public string vungleAppId = "5f410c269a3a9c00011623c3";

        public string placementId = "MENUBANNER-9074784";

        public VungleBannerAd()
        {
            this.InitializeComponent();
        }
    }
}