   M S B A M L   `   `   `  ����C  ?FuncDraw, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:FuncDrawFuncDraw  5   	       FuncDraw.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:FuncDraw     5   	   -   5          � Title$  FuncDraw��5   	   $	��450��6   $	��800��6'   .��5      � -   5         �Name$
 Grid��5       � ColumnDefinitions 5   
   �� 5      -   5         �Name$ mainGrid��5   +   $��3* �6   6+   �� 5      $	��37* �6   �� 5      -   5        $ editor��5   +   $	��200 �6   6+    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ MainGrid��5      
�������$��0��6;   $��0��6K   $��2��6�   ]� 5   
   -   5         ]�Name$ SideMenu��5      $��2��6&   $	��200��66   $��Center=�6C   
�������.+�5      � -   5        $ 	Container��5       5      �� 5      $
��20px �6'   �� 5      $��* �6'   .+�5      � $��1��6    5       �� 5!      $��* �6(   �� 5"      $	��100 �6(   .+�5$      �� -   5         ��Name$ ExpressionContainer��5$   !   $��1��6>   $��0��6N   �� 5%      -	   5         ��Name$ 
btnCentroj��5%   /   $��Centrój��6   $��Center=�6C   $��0,10,0,0q�6`   $��1��6r   $	��Top=�6   $	��160��6�   6�   �� 5&      -
   5        $ btnSkala��5&   6   $��Skala Domyślna��6   $��Center=�6H   $��1��6e   $��Center=�6r   $	��160��6�   	 J� RenderTransformOrigin$	 0.231,0.752&�6�   6�   �� 5'      -   5        $ btnSave��5'   .   $��Zapisz��6   $
��Left=�6?   $��	10,70,0,0q�6Z   $��1��6m   $	��Top=�6z   $��70��6�   $	 0.231,0.752&�6�   z�5)      V� 5*      .~�5+   "   �� 
 �� ScaleX$
 1Q�61   �� 5,   "   �� 5-   "   T� 5.   "   5/      51      �� 52      -   5        $ btnOpen��52   .   $��Otwórz��6   $
��Left=�6?   $��
100,70,0,0q�6Z   $��1��6n   $	��Top=�6{   $��70��6�   $	 0.231,0.752&�6�   z�54      V� 55      .~�56   "   �� $
 1Q�61   �� 57   "   �� 58   "   T� 59   "   5:      5<      5>      �� 5@      -   5        $ AddBtn��5F      $��1��5A      $��20��5B      $	��top=�5C      $��2,0,5,5q�5D      $��20��5E      $��+��5G      
�������
���   �
�������$��16(�6U   $
��Bold"�6c   $��0,0,6,0q�6u   $��Center=�6�   $
��Left=�6�   �� 5I      -   5        $ ShowHideBtn��5K      $��0��5J      $	��|||��5L      $��Center=�5M      $��Center=�5N      $	 0.5,0.5&�5O      $��20��5P      $��20��6    
�������
�������z�5T      V� 5U      .~�5V      �� �� 5W      �� 5X       �� Angle$ 90Q�6.   T� 5Y      5Z      5\      5a      5c      5e      5g      