   M S B A M L   `   `   `  ���� K  GTiCi Recognition, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TiCi_RecognitionTiCi_Recognition  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf !    TiCi_Recognition.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:TiCi_Recognition     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf       � Title$  
MainWindow��$
��1000��$
��1300�� � AllowsTransparency .  � WindowStyle$
 None=� � 
ResizeMode$ CanResizeWithGrip=� � WindowStartupLocation$ CenterScreen=�$��1q����#7A96D8.��� .+�� -    �Name$
 Logo��$
��Left=�$��95��$	��150��$	��Top=����#00C484� -   $ Left_Control��$
��Left=�$	��903��$	��150��$��0,95,0,0q�$	��Top=����#EDF2F8� -   $ Top_Control��$
��Left=�$��95��$
��1148��$��	150,0,0,0q�$	��Top=����#EA5D2F�� -    ��Name$
 Home��$
��Left=�$��66��$	��157��$��
193,14,0,0q�$	��Top=�
������ ���	#00000000$��	Open Sans)�$��23(�.���� -    ��Name$ HomeTB��Thống kê�� -   $ Config��$
��Left=�$��66��$	��173��$��
518,14,0,0q�$	��Top=�
������ ���	#00000000$��	Open Sans)�$��23(�.���� -   $ ConfigTB�� J� Opacity$	 0.3Q����� 	 �� Color$	 	#DDFFFFFF��Cài đặt�� -   $ Tutorial��$
��Left=�$��66��$	��176��$��
691,14,0,0q�$	��Top=�
������ ���	#00000000$��	Open Sans)�$��23(�.���� -	   $ 
TutorialTB��$	 0.3Q����� $	 	#DDFFFFFF��Hướng dẫn�� -
   $ Manager��$
��Left=�$��66��$	��157��$��
355,14,0,0q�$	��Top=�
������ ���	#00000000$��	Open Sans)�$��23(�.���� -   $ 	ManagerTB��$	 0.3Q����� $	 	#DDFFFFFF��
Quản lý� -   $ Home_Content��.+��� -   $ 	Dashboard��$
��Left=�$	��123��$��	0,131,0,0q�$	��Top=�$	��150��
������ ���	#FFFF7167$��20(�$��4,0,0,0q����	#FFFF7167.���� .+��� -   
 ��Name$
 DashboardIMG��$$��/Image/speedometer_clicked.png��$��57��$��98���� -   $ DashboardTB��$��Tổng quát��$	��120��$��Center=� �� TextAlignment$ Center=�$��33��$��19(��� -   $ Detail��$
��Left=�$	��123��$��	0,259,0,0q�$	��Top=�$	��150��
������ ���	#00000000$��20(�$��4,0,0,0q����	#FFFF7167.���� .+��� -   $
 	DetailIMG��$��Image/filechart.png��$��57��$��98���� -   $ DetailTB��$��
Chi tiết��$	��120��$��Center=�$ Center=�$��33�����	#FF787E7E$��19(��� -   $ Camera��$
��Left=�$	��123��$��	0,387,0,0q�$	��Top=�$	��150��
������ ���	#00000000$��20(�$��4,0,0,0q����	#FFFF7167.���� .+��� -   $
 	CameraIMG��$��Image/camera.png��$��57��$��98���� -   $ CameraTB��$��Camera��$	��120��$��Center=�$ Center=�$��33�����	#FF787E7E$��19(�� -   $ Dashboard_Content��$y�Hidden=�.+��� -    ��Name$ 
GraphLabel��$��Biểu đồ��$
��Left=�$��46��$��183,105,0,0q�$	��Top=�$	��125��$��	Open Sans)�$��25(�#  LiveCharts.Wpf.CartesianChart  &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$
 Left=�$��183,156,58,449q�$��15(�   Series �� *SeriesCollection+   AxisX   LiveCharts.Wpf.Axis     Title$ Đơn vị lớp��   Labels �� *Labels+   AxisY   $ Số lượng��   LabelFormatter �� *	Formatter+�� -   $ LogLabel��$��
Nhật ký��$
��Left=�$��46��$��183,572,0,0q�$	��Top=�$	��125��$��	Open Sans)�$��25(�(� -    (�Name$	 Log��$
��Left=�$	��375��$	��Top=�$	��483��$��183,623,0,0q�.���� ���	#FFFF7167$��5,0,0,0q�._��� -    ��Name$
 Logs��
���   �$��5,0,0,0q�$	��579��$	��743��$��60(�.���� -    ��Name$
 mySP�� �� Orientation$ Vertical=��� -   $ CameraLabel��$��Camera chính��$
��Left=�$��46��$��716,572,0,0q�$	��Top=�$	��180��$��	Open Sans)�$��25(��� -   $
 CameraViewer_small��$
��Left=�$	��375��$��722,622,0,0q�$	��Top=�$	��518��� -   $ Detail_Content��� -   $ Camera_Content��.+��� -    $
 CameraView_large��$
��Left=�$	��903��$��
150,95,0,0q�$	��Top=�$
��1148��� -!   $ Manager_Content��.+��� -"   $	 Add��$
��Left=�$	��123��$��	0,131,0,0q�$	��Top=�$	��150��
������ ���	#FFD500F9$��20(�$��4,0,0,0q����	#00000000.���� .+��� -#   $
 AddIMG��$��Image/add_clicked.png��$��57��$��98���� -$   $ AddTB��$��Nạp dữ liệu��$	��120��$��Center=�$ Center=�$��33��$��19(����	#FFD500F9�� -%   $ StudentDetail��$
��Left=�$	��123��$��	0,259,0,0q�$	��Top=�$	��150��
������ ���	#00000000$��20(�$��4,0,0,0q����	#FFFF7167.���� .+��� -&   $
 StudentDetailIMG��$��Image/studentdetail.png��$��57��$��98���� -'   $ StudentDetailTB��$��
Học sinh��$	��120��$��Center=�$ Center=�$��33�����	#FF787E7E$��19(��� -(   $
 Data��$
��Left=�$	��123��$��	0,387,0,0q�$	��Top=�$	��150��
������ ���	#00000000$��20(�$��4,0,0,0q����	#FFFF7167.���� .+��� -)   $
 DataIMG��$��Image/database.png��$��57��$��98���� -*   $ DataTB��$��Số liệu��$	��120��$��Center=�$ Center=�$��33�����	#FF787E7E$��19(�� -+   $ Config_Content��.+��� -,   $ 
AccountCon��$
��Left=�$	��123��$��	0,131,0,0q�$	��Top=�$	��150��
������ ���	#FF448AFF$��20(�$��4,0,0,0q����	#00000000.���� .+��� --   $
 AccountConIMG��$��Image/account_clicked.png��$��57��$��98���� -.   $ AccountConTB��$��Tài khoản��$	��120��$��Center=�$ Center=�$��33��$��19(����	#FF448AFF�� -/   $ 	SystemCon��$
��Left=�$	��123��$��	0,259,0,0q�$	��Top=�$	��150��
������ ���	#00000000$��20(�$��4,0,0,0q����	#FFFF7167.���� .+��� -0   $
 SystemConIMG��$��Image/system.png��$��57��$��98���� -1   $ SystemConTB��$��Hệ thống��$	��120��$��Center=�$ Center=�$��33�����	#FF787E7E$��19(�� -2   $ Tutorial_Content��