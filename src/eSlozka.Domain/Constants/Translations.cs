namespace eSlozka.Domain.Constants;

public static class Translations
{
    public const string ResourcesBase = "";

    public static class MainLayout
    {
        public const string MainLayoutBase = ResourcesBase + nameof(MainLayout);

        public static class Appbar
        {
            public const string AppbarBase = MainLayoutBase + nameof(Appbar);

            public const string Brand = AppbarBase + nameof(Brand);

            public static class Drawer
            {
                public const string AccountDrawerBase = AppbarBase + nameof(Drawer);

                public static class Link
                {
                    public const string LinkBase = AccountDrawerBase + nameof(Link);

                    public const string Account = LinkBase + nameof(Account);
                    public const string Settings = LinkBase + nameof(Settings);
                    public const string Logout = LinkBase + nameof(Logout);
                }
            }
        }

        public static class Sidebar
        {
            public const string SidebarBase = MainLayoutBase + nameof(Sidebar);

            public static class Drawer
            {
                public const string DrawerBase = SidebarBase + nameof(Drawer);

                public static class Link
                {
                    public const string LinkBase = DrawerBase + nameof(Link);

                    public const string Files = LinkBase + nameof(Files);
                    public const string SharedWithMe = LinkBase + nameof(SharedWithMe);
                    public const string Bookmarks = LinkBase + nameof(Bookmarks);
                    public const string Deleted = LinkBase + nameof(Deleted);
                    public const string Notifications = LinkBase + nameof(Notifications);
                    public const string ManageUsers = LinkBase + nameof(ManageUsers);
                    public const string ManageRoles = LinkBase + nameof(ManageRoles);
                    public const string ManageRepository = LinkBase + nameof(ManageRepository);
                    public const string ManageCodeLists = LinkBase + nameof(ManageCodeLists);
                    public const string ManageTasks = LinkBase + nameof(ManageTasks);
                    public const string ManageStatistics = LinkBase + nameof(ManageStatistics);
                    public const string History = LinkBase + nameof(History);
                    public const string Logs = LinkBase + nameof(Logs);
                    public const string Account = LinkBase + nameof(Account);
                    public const string Settings = LinkBase + nameof(Settings);
                    public const string AboutApplication = LinkBase + nameof(AboutApplication);
                    public const string Logout = LinkBase + nameof(Logout);
                }
            }
        }
    }

    public static class Login
    {
        public const string LoginBase = ResourcesBase + nameof(Login);

        public const string Title = LoginBase + nameof(Title);

        public static class EmailInput
        {
            public const string EmailInputBase = LoginBase + nameof(EmailInput);
            public const string Label = EmailInputBase + nameof(Label);
            public const string Placeholder = EmailInputBase + nameof(Placeholder);
        }

        public static class PasswordInput
        {
            public const string PasswordInputBase = LoginBase + nameof(PasswordInput);
            public const string Label = PasswordInputBase + nameof(Label);
            public const string AdornmentArialLabel = PasswordInputBase + nameof(AdornmentArialLabel);
        }

        public static class LoginButton
        {
            public const string LoginButtonBase = LoginBase + nameof(LoginButton);
            public const string Text = LoginButtonBase + nameof(Text);
        }

        public static class RegisterButton
        {
            public const string RegisterButtonBase = LoginBase + nameof(RegisterButton);
            public const string Text = RegisterButtonBase + nameof(Text);
        }
    }

    public static class Register
    {
        public const string RegisterBase = ResourcesBase + nameof(Register);

        public const string Title = RegisterBase + nameof(Title);

        public static class GivenNameInput
        {
            public const string GivenNameBase = RegisterBase + nameof(GivenNameInput);
            public const string Label = GivenNameBase + nameof(Label);
            public const string Placeholder = GivenNameBase + nameof(Placeholder);
        }

        public static class FamilyNameInput
        {
            public const string FamilyNameBase = RegisterBase + nameof(FamilyNameInput);
            public const string Label = FamilyNameBase + nameof(Label);
            public const string Placeholder = FamilyNameBase + nameof(Placeholder);
        }

        public static class EmailInput
        {
            public const string EmailBase = RegisterBase + nameof(EmailInput);
            public const string Label = EmailBase + nameof(Label);
            public const string Placeholder = EmailBase + nameof(Placeholder);
        }

        public static class PasswordInput
        {
            public const string PasswordBase = RegisterBase + nameof(PasswordInput);
            public const string Label = PasswordBase + nameof(Label);
            public const string AdornmentAriaLabel = PasswordBase + nameof(AdornmentAriaLabel);
        }

        public static class PasswordRepeatInput
        {
            public const string PasswordRepeatBase = RegisterBase + nameof(PasswordRepeatInput);
            public const string Label = PasswordRepeatBase + nameof(Label);
            public const string AdornmentAriaLabel = PasswordRepeatBase + nameof(AdornmentAriaLabel);
        }

        public static class LoginButton
        {
            public const string LoginButtonBase = RegisterBase + nameof(LoginButton);
            public const string Text = LoginButtonBase + nameof(Text);
        }

        public static class RegisterButton
        {
            public const string RegisterButtonBase = RegisterBase + nameof(RegisterButton);
            public const string Text = RegisterButtonBase + nameof(Text);
        }
    }

    public static class Validation
    {
        public const string ValidationBase = ResourcesBase + nameof(Validation);

        public static class User
        {
            public const string UserBase = ValidationBase + nameof(User);

            public static class Email
            {
                public const string EmailBase = UserBase + nameof(Email);

                public const string Required = EmailBase + nameof(Required);
                public const string MaxLength = EmailBase + nameof(MaxLength);
                public const string Format = EmailBase + nameof(Format);
                public const string Exists = EmailBase + nameof(Exists);
            }

            public static class GivenName
            {
                public const string GivenNameBase = UserBase + nameof(GivenName);

                public const string Required = GivenNameBase + nameof(Required);
                public const string MaxLength = GivenNameBase + nameof(MaxLength);
            }

            public static class FamilyName
            {
                public const string FamilyNameBase = UserBase + nameof(FamilyName);

                public const string Required = FamilyNameBase + nameof(Required);
                public const string MaxLength = FamilyNameBase + nameof(MaxLength);
            }

            public static class Password
            {
                public const string PasswordBase = UserBase + nameof(Password);

                public const string Required = PasswordBase + nameof(Required);
                public const string MaxLength = PasswordBase + nameof(MaxLength);
                public const string MinLength = PasswordBase + nameof(MinLength);
                public const string LowerCaseCharacter = PasswordBase + nameof(LowerCaseCharacter);
                public const string UpperCaseCharacter = PasswordBase + nameof(UpperCaseCharacter);
                public const string SpecialCharacter = PasswordBase + nameof(SpecialCharacter);
                public const string NumericCharacter = PasswordBase + nameof(NumericCharacter);
            }

            public static class PasswordRepetition
            {
                public const string PasswordRepetitionBase = UserBase + nameof(PasswordRepetition);

                public const string Required = PasswordRepetitionBase + nameof(Required);
                public const string MaxLength = PasswordRepetitionBase + nameof(MaxLength);
                public const string MinLength = PasswordRepetitionBase + nameof(MinLength);
                public const string Match = PasswordRepetitionBase + nameof(Match);
            }
        }
    }
}
