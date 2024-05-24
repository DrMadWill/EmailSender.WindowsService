namespace EmailWService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DrServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.DrServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // DrServiceProcessInstaller
            // 
            this.DrServiceProcessInstaller.Password = null;
            this.DrServiceProcessInstaller.Username = null;
            
            // Local Sistem adding | DrMadWill
            this.DrServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            // 
            // DrServiceInstaller
            // 

            // Sistem Property Adding | DrMaddWill
            this.DrServiceInstaller.ServiceName = "DrMadWill.Service";
            this.DrServiceInstaller.DisplayName = "DrMadWillEmailSender";
            this.DrServiceInstaller.Description = "Email Sender From DrMadWil";

            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DrServiceProcessInstaller,
            this.DrServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller DrServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller DrServiceInstaller;
    }
}