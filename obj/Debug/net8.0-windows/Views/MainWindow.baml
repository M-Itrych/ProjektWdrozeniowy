   M S B A M L   `   `   `  ����U  QScottPlot.WPF, Version=5.0.46.0, Culture=neutral, PublicKeyToken=86698dc10387c39eD2clr-namespace:ScottPlot.WPF;assembly=ScottPlot.WPFScottPlot.WPF  5   	   M IProjektWdrozeniowy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ProjektWdrozeniowyProjektWdrozeniowy 5   	   #   ProjektWdrozeniowy.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   H	ScottPlot2clr-namespace:ScottPlot.WPF;assembly=ScottPlot.WPF     5   	   2local clr-namespace:ProjektWdrozeniowy     5   	     � Title$  GraphMasters��5	   	   $	��450��6   $	��800��6+    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	   ���#181735.��5      � -   5         � RowDefinitions 5   
   �� 5      $��25 �6   �� 5       � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      .+�5   
   � $��1��6   $��1��6    5      �� 5      $��50 �6    �� 5      .+�5      � ���#0f0f2d$��0��6(   .+�5      �� -   5        $��countrySelect��5      $	��200��60   $��30��6<   $��Center=�6H   $��Center=�6c   ��5      �� *	Countries+��5       �� 5!      .R�5"      �� -   5        $��Stretch=�5$   '   $��Center=�5%   '   ��5"   '   �� *	Country+ d� 	IsChecked 5#   '   �� *
IsSelected+5&   '   5'      ��5*      �� 5+       �� 
TargetType" ��6    .�5,      ��  �� Property � �� �� Value$ Stretch=�6K   �� 5-       � ��$ Center=�6I   5.      50      51      � 52      -   5        $��PlotGrid��52       $��1��6   53      54      � 55   
   ���#0f0f2d$��2��56       57      �� 58      �� 59      �� 5:      �� 5;      �� 5<      �� 5=      �� 5>      �� 5?      �� 5@      �� 5A      �� 5B      �� 5C      �� 5D      .+�5F      �� $ ��/Assets/Images/AppLogo.png��6   $��6��68   �� 5G      $��11��6   $��Center=�6)   $��Center=�6D   $��Medium"�6a   �� 5H      -   5        $��https://github.com?�5H      	 �� TextDecorations$
	 None{�6Z   
�������.�6�   GitHub5J      5K      �� 5L      -   5        $��X��5L      $��12��5M      $��20��5N      $��20��5O      $��0q�5P      
�������
 � Cursor$

 Hand��5S      ��5T      �� 5U       �� 
TargetType" ��6&   .R�5V      �� ���	#FF630000 �� CornerRadius$ 20��5W   !   ._�5X      �� $��Center=�6/   $��Center=�5Y   /   5Z      5[      5]      5^      o� 5_   
   $��1��6   ���#0f0f2d.@�5`      �� $��Wybierz typ:��6   $��
ExtraBlack"�6,   
�������6D   m� 5a      $�� ��6   $	��100��6!   ${�False��6-   m� 5b      -   5        $��Option 1��5b      $��Medium"�6)   
�������6P   m� 5c      -   5        $��Option 2��5c      $��Medium"�6)   
�������6P   m� 5d      -	   5        $��Option 3��5d      $��Medium"�6)   
�������6P   m� 5e      -
   5        $��Option 4��5e      $��Medium"�6)   
�������6P   m� 5f      -   5        $��Option 5��5f      $��Medium"�6)   
�������6P   5g      5h      5i      