   M S B A M L   `   `   `  ����H  DDungeonEscape, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DungeonEscapeDungeonEscape  5   	       DungeonEscape.frmMonster   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:DungeonEscape     5   	     � Title$  Yikes! A Monster!��5   	   $	��250��6#   $	��300��60   $��Monotype Corsiva)�6<   $��14(�6Z    � WindowStartupLocation$ CenterScreen=�6h    � 
ResizeMode$ NoResize=�6�   ��5	      
� 5
   
   .��5      �  � Color$ Black��6    � Offset$ 0Q�6)   � 5      $ 	#FF6E0000��6   $ 1Q�6-   5      .��5      �  � ColumnDefinitions 5   
   �� 5      .+�5   
   �� -   5         ��Name$ 	tbMonster��5      $
��Left=�6'   $��
161,34,0,0q�6B   $
��Wrap=�6V   $	��Top=�6j   $��73��6�   $	��116��6�   $��Monotype Corsiva)�6�   $��20(�6�    �� TextAlignment$ Center=�6�   
�������6�   �� 5   
   -   5        $ tbHero��5      $
��Left=�6$   $��	16,34,0,0q�6?   $
��Wrap=�6R   $	��Top=�6f   $��73��6~   $	��116��6�   $��Monotype Corsiva)�6�   $��20(�6�   $ Center=�6�   
�������6�   �� 5   
   -   5        $ 
tbWhatToDo��5      $
��Left=�6(   $��
43,125,0,0q�6C   $
��Wrap=�6W   $��What do you want to do?��6k   $	��Top=�6�   $��23��6�   $	��205��6�   $ Center=�6�   $��20(�6�   
�������6�   �� 5   
   -   5         ��Name$ 	btnAttack��5      $��Attack��6$   $
��Left=�65   $��
43,166,0,0q�6P   $	��Top=�6d   $��65��6|   $��25��6�   $��16(�6�   �� 5   
   -   5        $ 
btnRunAway��5      $��Run away��6%   $
��Left=�68   $��183,166,0,0q�6S   $	��Top=�6h   $��65��6�   $��25��6�   $��16(�6�   5      5      