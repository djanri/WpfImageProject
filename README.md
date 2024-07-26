# WpfImageProject

This project is going to show you two ways of loading images in the Image control.

The first approach is simpler in implementation. All you need is to set the Source property to the path of the image file. The main feature here is the source file is **locked**. You can't delete it or reuse it after loading to the Image control. If you'll try to do it you get the message like "The file is used by another process".

![image](https://github.com/user-attachments/assets/fe2afefa-dab9-4b7c-8e62-05b5675397e1)


The second approach is more difficult. And you get possibility to reuse the image file or even delete it. How to do it? You need to load the image file to **BitmapImage** in code. Do not forget to set **CacheOption** to **OnLoad** value!

![image](https://github.com/user-attachments/assets/732bb709-1801-42d7-9f1e-5f4614e7401d)
