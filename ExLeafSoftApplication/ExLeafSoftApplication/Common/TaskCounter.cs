using Acr.UserDialogs;
using ExLeafSoftApplication.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExLeafSoftApplication.Common
{
    public class TaskCounter
    {
        //https://stackoverflow.com/questions/45209784/how-can-i-run-a-method-in-the-background-on-my-xamarin-app/45210187
        //https://xamarinhelp.com/xamarin-background-tasks/

        private IUserDialogs Dialogs { get { return UserDialogs.Instance; } }



        //public async Task TimerRunCounter()
        //{
        //    await Task.Run(async () =>
        //    {

        //        using (var dlg = this.Dialogs.Progress("Synchronization"))
        //        {

        //            while (1==1)
        //            {


        //                SynchronizationService service = new SynchronizationService();

        //                List<CompactCustomerModel> inserted = await service.SendLocalFarmerAsync();
        //                if (inserted != null && inserted.Count > 0)
        //                    App.FarmerTable.BatchUpdateInsertedAsync(inserted, true);
        //                await Task.Delay(TimeSpan.FromSeconds(1));
        //                dlg.PercentComplete += 25;

        //                List<CompactCustomerModel> updated = await service.SendUpdatedFarmerAsync();
        //                if (updated != null && updated.Count > 0)
        //                {
        //                    App.FarmerTable.BatchUpdateInsertedAsync(updated, false);
        //                }
        //                await Task.Delay(TimeSpan.FromSeconds(1));
        //                dlg.PercentComplete += 25;

        //                List<ServerCustomerModel> serverRecords = await service.FetchServerFarmerAsync();
        //                if (serverRecords != null && serverRecords.Count > 0)
        //                {
        //                    App.FarmerTable.BatchInsert(serverRecords);
        //                    App.AddressFarmerTable.BacthInsert(App.FarmerTable.farmerAddresslist);

        //                }
        //                await Task.Delay(TimeSpan.FromSeconds(1));
        //                dlg.PercentComplete += 25;

        //                //string b = await service.FetchServerFarmerAsync();


        //                await Task.Delay(TimeSpan.FromSeconds(1));

        //                dlg.PercentComplete += 25;
        //                //dlg.Title = "Synch - " + dlg.PercentComplete.ToString();

        //                if (dlg.PercentComplete >= 100)
        //                {
        //                    break;
        //                }
        //                //await Task.Delay(TimeSpan.FromMilliseconds(100));
        //                //var message = new TickedMessage
        //                //{
        //                //    Message = counter.ToString()
        //                //};
        //                //Device.BeginInvokeOnMainThread(() =>
        //                //        {
        //                //            MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
        //                //        });

                      
                         
                           
                        
        //            }

        //            dlg.Hide();
        //            await Task.Delay(TimeSpan.FromSeconds(1));
        //            dlg.Dispose();
                   
                  





        //        }
        //        App.timer.Start();





        //    });
        //}

        public async Task RunCounter(CancellationToken token)
        {
            await Task.Run(async () => {
                
                using (var dlg = this.Dialogs.Progress("Synchronization"))
                {
                    
                    while (dlg.PercentComplete <= 100)
                    {
                        token.ThrowIfCancellationRequested();


                        //PhotoService ser = new PhotoService();
                         //await ser.SendPhotoAsync();


                        SynchronizationService service = new SynchronizationService();

                        List<CompactCustomerModel> inserted = await service.SendLocalFarmerAsync();
                        if(inserted != null && inserted.Count > 0)
                        App.FarmerTable.BatchUpdateInsertedAsync(inserted,true);

                        await Task.Delay(TimeSpan.FromSeconds(1));

                        dlg.PercentComplete += 15;
                        dlg.Title = "Synch - " + dlg.PercentComplete.ToString();

                        List<CompactCustomerModel> updated = await service.SendUpdatedFarmerAsync();
                        if (updated != null && updated.Count > 0)
                        {
                            App.FarmerTable.BatchUpdateInsertedAsync(updated,false);
                        }

                        await Task.Delay(TimeSpan.FromSeconds(1));

                        dlg.PercentComplete += 15;
                        dlg.Title = "Synch - " + dlg.PercentComplete.ToString();

                        List<ServerCustomerModel> serverRecords = await service.FetchServerFarmerAsync();
                        if(serverRecords != null && serverRecords.Count > 0)
                        {
                            App.FarmerTable.BatchInsert(serverRecords);
                            //if(App.FarmerTable.farmerAddresslist.Count > 0)
                            //App.AddressFarmerTable.BacthInsert(App.FarmerTable.farmerAddresslist);

                        }
                        
                        //string b = await service.FetchServerFarmerAsync();

                        await Task.Delay(TimeSpan.FromSeconds(1));
                        dlg.PercentComplete += 15;
                        dlg.Title = "Synch - " + dlg.PercentComplete.ToString();

                        List<CompactFieldModel> insertedFields = await service.SendLocalFieldAsync();
                        if (insertedFields != null && insertedFields.Count > 0)
                        {

                            App.FieldTable.BatchUpdateInsertedAsync(insertedFields,true);
                        }

                        await Task.Delay(TimeSpan.FromSeconds(1));

                        dlg.PercentComplete += 15;
                        dlg.Title = "Synch - " + dlg.PercentComplete.ToString();

                        List<CompactFieldModel> updatedFields = await service.SendUpdatedFieldAsync();
                        if (updatedFields != null && updatedFields.Count > 0)
                        {

                            App.FieldTable.BatchUpdateInsertedAsync(insertedFields, false);
                        }

                        await Task.Delay(TimeSpan.FromSeconds(1));

                        dlg.PercentComplete += 15;
                        dlg.Title = "Synch - " + dlg.PercentComplete.ToString();

                        List<ServerFieldModel> serverFields = await service.FetchServerFieldAsync();
                        if (serverFields != null && serverFields.Count > 0)
                        {

                            App.FieldTable.BatchInsert(serverFields);
                        }

                        await Task.Delay(TimeSpan.FromSeconds(1));

                        dlg.PercentComplete += 25;
                        dlg.Title = "Synch - " + dlg.PercentComplete.ToString();


                        //await Task.Delay(TimeSpan.FromMilliseconds(100));
                        //var message = new TickedMessage
                        //{
                        //    Message = counter.ToString()
                        //};
                        //Device.BeginInvokeOnMainThread(() =>
                        //        {
                        //            MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
                        //        });

                        if (dlg.PercentComplete >= 100)
                        {
                            var Stopmessage = new StopLongRunningTaskMessage();
                            MessagingCenter.Send(Stopmessage, "StopLongRunningTaskMessage");
                            
                        }
                    }

                    dlg.Dispose();


            


                }

               

                //for (long i = 1; i < long.MaxValue; i++)
                //{
                //    token.ThrowIfCancellationRequested();

                //    await Task.Delay(250);
                //    var message = new TickedMessage
                //    {
                //        Message = i.ToString()
                //    };

                //    if (i == 100)
                //    {

                //        var Stopmessage = new StopLongRunningTaskMessage();
                //        MessagingCenter.Send(Stopmessage, "StopLongRunningTaskMessage");
                //        App.timer.Start();
                //    }
                //    else
                //    {

                //        Device.BeginInvokeOnMainThread(() =>
                //        {
                //            MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
                //        });
                //    }
                //}
            }, token);

            GC.Collect();
            GC.WaitForPendingFinalizers();

        }
    }
}
