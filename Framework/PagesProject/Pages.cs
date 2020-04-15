using Core.Helpers;
using ProjectToTest.pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToTest
{
    public static class Pages
    {
        public static HomePage HomePage
        {
            get { return PageFactoryHelper.InitElements<HomePage>(); }
        }
    }
}
