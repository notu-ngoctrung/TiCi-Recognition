   M S B A M L   `   `   `  ����K  GTiCi Recognition, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TiCi_RecognitionTiCi_Recognition  5   	   #    TiCi_Recognition.AddEditClass   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:TiCi_Recognition     5   	     � Title$  AddEditClass��5   	   $	��450��6   $	��500��6+    � WindowStartupLocation$ CenterScreen=�67   .��5	      � .+�5
   
   �� -   5         ��Name$ AddLabel��5
      $��Tạo mới��6"   $
��Left=�64   $��42��6O   $��9,21,0,0q�6[   $	��Top=�6m   $	��157��6�   $��	Open Sans)�6�   $��20(�6�   ���	#FF424242   	SubButton#���   6�   $��4,0,0,0q�6�   $��10,5,5,5q�6  �� 5   
   -   5         ��Name$ AddBox��5      $
��Left=�6"   $��44��6=   $��	45,81,0,0q�6I    �� TextWrapping$
 Wrap=�6\   $��(nhập tên lớp)��6p   $	��Top=�6�   $	��289��6�   $��16(�6�   $��	Open Sans)�6�   $��0,0,0,6q�6�   6�   �� 5   
   -   5         ��Name$ 	AddButton��5      $��Thêm��6$   $
��Left=�63   $��44��6N   $��
367,71,0,0q�6Z   $	��Top=�6n   $��81��6�     Gradient#���  6�   $��	Open Sans)�6�   $��16(�6�   $��0q�6�   6�   �� 5   
   -   5        $ 	EditLabel��5      $��Chỉnh sửa��6#   $
��Left=�67   $��42��6R   $��	9,156,0,0q�6^   $	��Top=�6q   $	��157��6�   $��	Open Sans)�6�   $��20(�6�   ���	#FF424242#���   6�   $��4,0,0,0q�6�   $��10,5,5,5q�6  �� 5   
   -   5        $ 	EditNote1��5      $V��PLưu ý: Chọn tên lớp cần chỉnh sửa và điền tên mới vào khung��6#   $
��Left=�6i   $��38��6�   $	��Top=�6�   $	��425��6�   $��	Open Sans)�6�   $��15(�6�   $��5,5,5,5q�6�   $��Italic$�6�   
���  ��$��
26,201,0,0q�6  �� 5   
   -   5        $ 	EditNote2��5      $C��=bên dưới. Nếu xóa lớp hiện có thì để trống.��6#   $
��Left=�6Y   $��38��6t   $	��Top=�6�   $	��362��6�   $��	Open Sans)�6�   $��15(�6�   $��5,5,5,5q�6�   $��Italic$�6�   
���  ��$��
70,218,0,0q�6�   �� 5   
   -   5         ��Name$ EditBox1��5      $
��Left=�6%   $��51��6@   $��
45,271,0,0q�6L   $	��Top=�6`   $	��148��6x   $��	Open Sans)�6�   $��18(�6�   $��6,3,5,0q�6�   �� 5   
   -   5        $ EditBox2��5      $
��Left=�6$   $��51��6?   $��254,271,0,0q�6K   $
 Wrap=�6`   $��(nhập tên mới)��6t   $	��Top=�6�   $	��197��6�   $��16(�6�   $��	Open Sans)�6�   $��0,0,0,6q�6�   6�   �� 5   
   -	   5        $ AddButton_Copy��5      $��
Thực thi��6)   $
��Left=�6<   $��44��6W   $��232,353,0,0q�6c   $	��Top=�6x   $	��103��6�   #���  6�   $��	Open Sans)�6�   $��16(�6�   $��0q�6�   6�   �� 5   
   -
   5        $ AddButton_Copy1��5      $��Reset��6*   $
��Left=�6:   $��44��6U   $��350,353,0,0q�6a   $	��Top=�6v   $��98��6�   #���  6�   $��	Open Sans)�6�   $��16(�6�   $��0q�6�   5      5      