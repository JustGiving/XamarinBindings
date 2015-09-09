﻿using MonoTouch.Dialog;
using TrustDefenderSDK.iOS;
using Foundation;
using UIKit;

namespace TrustDefender.iOS.SampleApp
{
  public class MainViewController : DialogViewController, ITrustDefenderMobileDelegate
  {
    TrustDefenderMobile trustDefender;

    public MainViewController()
      : base(UITableViewStyle.Plain, null)
    {
      var initDictionary = new NSDictionary("org_id", "pdj3oyez", "delegate", this, "location_services", true, "timeout", 10000);

      trustDefender = new TrustDefenderMobile(initDictionary);

      Root = new RootElement("TrustDefenderMobile")
      {
        new Section("Profiling")
        {
          new StringElement("Do Profile Request", () =>
            {
              var responce = trustDefender.DoProfileRequest();
              ShowAlert(responce.ToString());
            }),
          new StringElement("Do Profile Request (options)", () =>
            {
              var responce = trustDefender.DoProfileRequest(new NSDictionary("session_id", "id"));
              ShowAlert(responce.ToString());
            }),
          new StringElement("Do Profile Request with Callback", () =>
            {
              var responce = trustDefender.DoProfileRequestWithCallback(Callback);
              ShowAlert(responce.ToString());
            }),
          new StringElement("Do Profile Request with Options", () =>
            {
              var responce = trustDefender.DoProfileRequestWithOptions(new NSDictionary("session_id", "id"), Callback);
              ShowAlert(responce.ToString());
            }),
        },
        new Section("Getters")
        {
          new StringElement("Get Result", () =>
            {
              var result = trustDefender.Result;
              
              ShowAlert(result.ToString());
            }),
          new StringElement("Get Version", () =>
            {
              var version = trustDefender.Version;
              ShowAlert(version);
            })
        },
        new Section("Other")
        {
          new StringElement("Pause true", () => trustDefender.PauseLocationServices(true)),
          new StringElement("Pause false", () => trustDefender.PauseLocationServices(true)),
          new StringElement("Cancel", trustDefender.Cancel),       
        }
      };
    }

    void Callback(NSDictionary dictionary)
    {
      System.Console.WriteLine("Callback: " + dictionary);
    }

    void ShowAlert(string message)
    {
      new UIAlertView("Message", message, null, "Ok").Show();
    }

    public void ProfileComplete(NSDictionary profileResults)
    {
      System.Console.WriteLine("inside profileComplete: " + profileResults);
    }

    public override void ViewWillUnload()
    {
      base.ViewWillUnload();

      trustDefender.Cancel();
    }
  }
}