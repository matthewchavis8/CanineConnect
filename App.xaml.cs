﻿using CanineConnect.Pages;

namespace CanineConnect;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new LoginPage());
	}
}
