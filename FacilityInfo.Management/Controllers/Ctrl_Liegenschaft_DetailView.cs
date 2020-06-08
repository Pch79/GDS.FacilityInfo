using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using FacilityInfo.Anlagen.BusinessObjects;
using FacilityInfo.Liegenschaft.BusinessObjects;
using FacilityInfo.Management.BusinessObjects;

namespace FacilityInfo.Management.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Ctrl_Liegenschaft_DetailView : ViewController
    {
        public Ctrl_Liegenschaft_DetailView()
        {
            InitializeComponent();
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(boLiegenschaft);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            //der simple choice action die Items hinzufügen
            //die Technikeinheiten laden und zur Auswahl hinzufügen
            IObjectSpace os = this.ObjectSpace;
            DetailView curView = (DetailView)View;
            boLiegenschaft curLiegenschaft = os.GetObjectByKey<boLiegenschaft>(((boLiegenschaft)curView.CurrentObject).Oid);
            //this.doAddHaustechnikKomponente.

            if (curLiegenschaft != null)
            {
                if (curLiegenschaft.lstHaustechnikKomponenten != null)
                {
                    createMenue(this.doAddHaustechnikKomponente);
                    createMenue(this.doDeleteAnlagenGruppe);
                }
            }
            createMenue(this.doAddAnlage);
        }

        private void createMenue(SingleChoiceAction action)
        {
            #region Anlagen hinzufügen
            IObjectSpace os = this.ObjectSpace;
            DetailView curView = (DetailView)View;
            boLiegenschaft curLiegenschaft = os.GetObjectByKey<boLiegenschaft>(((boLiegenschaft)curView.CurrentObject).Oid);
            IList<boAnlagenArt> lstAnlagenArten = os.GetObjects<boAnlagenArt>(new BinaryOperator("Aktiv", true, BinaryOperatorType.Equal));
            IList<LgHaustechnikKomponente> lstLgKomponenten = null;
            if (curLiegenschaft != null)
            {
                if (curLiegenschaft.lstHaustechnikKomponenten != null)
                {
                    lstLgKomponenten = curLiegenschaft.lstHaustechnikKomponenten;
                }
            }
            switch (action.Id)
            {
                case ("doDeleteAnlagenGruppe"):
                    doDeleteAnlagenGruppe.Items.Clear();
                    foreach(LgHaustechnikKomponente item in curLiegenschaft.lstHaustechnikKomponenten)
                    {
                        ChoiceActionItem curActionItem = new ChoiceActionItem();
                        
                        curActionItem.Caption = (item.Bezeichnung!=null)?item.Bezeichnung:item.BezeichnungIntern;
                        curActionItem.Data = item;
                        doDeleteAnlagenGruppe.Items.Add(curActionItem);
                        

                    }
                    break;
                    //ANlagengruppe hinzufügen
                case ("doAddHaustechnikKomponente"):
                    #region Technikeinheiten
                    
                    IList<fiTechnikeinheit> lstTechnikeinheiten = os.GetObjects<fiTechnikeinheit>(new BinaryOperator("Aktiv", true, BinaryOperatorType.Equal));
                    //erst die Basisanlagen durchgehen
                    var lstBasisanlagen = (from fiTechnikeinheit item in lstTechnikeinheiten
                                          select item.Basisanlage).ToList();
                    var groupedList = lstTechnikeinheiten.GroupBy(t => t.Basisanlage).Select(t => t);
                    /*
                    var groupedList = item.Debitorenposition.lstBuchungspositionen.GroupBy(t => t.ArtikelNummer).Select(t => t);
                    */
                    foreach (var group in groupedList)
                    {
                        var title = group.Key;
                       
                        //jede Grupppe als Basis hinzufügen
                        ChoiceActionItem curGroupItem = new ChoiceActionItem(title.ToString(), null);
                        foreach(var item in group)
                        {
                            curGroupItem.Items.Add(new ChoiceActionItem(((fiTechnikeinheit)item).Oid.ToString(), ((fiTechnikeinheit)item).Bezeichnung, (fiTechnikeinheit)item));
                        }
                        this.doAddHaustechnikKomponente.Items.Add(curGroupItem);

                    }
                   
                    #endregion
                    break;
                case ("doAddAnlage"):
                    //this.doAddAnlage.Items.Add(new ChoiceActionItem("-Komponenten-", null));
                    doAddAnlage.Items.Clear();
                    
                    if (lstLgKomponenten != null)
                    {
                        foreach (LgHaustechnikKomponente htEntry in lstLgKomponenten)
                        {
                            ChoiceActionItem mainItem = new ChoiceActionItem(htEntry.Oid.ToString(), String.Format("{0} - {1}", htEntry.Systembezeichnung,htEntry.Bezeichnung), htEntry);
                            mainItem.ImageName = "control_panel_16";
                            mainItem.BeginGroup = true;


                            //hier nur die Anlagenarten die erlaubt sind
                            //htEntry.Technikeinheit.lstAnlagenarten
                            //gibt es noch andere Definitionen für die Technikeinheiten??
                            //Technikeinheit
                            //alle anlagenarten mit der gleichen Basisanlage
                            boAnlagenArt curBasisAnlage = os.GetObjectByKey<boAnlagenArt>(htEntry.Technikeinheit.Basisanlage.Oid);
                            //alle Technikeinheiten mit der selben Basisanlage
                            IList<fiTechnikeinheit> lstRelatedEinheiten = os.GetObjects<fiTechnikeinheit>(new BinaryOperator("Basisanlage.Oid", htEntry.Technikeinheit.Basisanlage.Oid, BinaryOperatorType.Equal));

                            List<boAnlagenArt> lstAllowedArten = new List<boAnlagenArt>();
                            foreach (fiTechnikeinheit einheit in lstRelatedEinheiten)
                            {
                                foreach (boAnlagenArt art in einheit.lstAnlagenarten)
                                {
                                    if (!lstAllowedArten.Contains(art))
                                    {
                                        lstAllowedArten.Add(art);
                                    }
                                }
                            }
                            //die Liste der Anlagenarten erstellen

                            //foreach (boAnlagenArt allowedArt in htEntry.Technikeinheit.lstAnlagenarten)
                            //die Gruppen die aktiv sind als mainitem setzen

                            foreach (boAnlagenArt allowedArt in lstAllowedArten)
                            {
                                ChoiceActionItem curItem = new ChoiceActionItem(allowedArt.Oid.ToString(), allowedArt.Bezeichnung, allowedArt);
                                curItem.ImageName = "control_panel_16";

                                mainItem.Items.Add(curItem);

                            }
                            /// mainItem.Items.Add(new ChoiceActionItem)
                            this.doAddAnlage.Items.Add(mainItem);
                        }
                    }
                    else
                    {

                        try
                        {
                            //hier die Gruppen einbauen
                            if (curLiegenschaft != null)
                            {
                                ChoiceActionItem mainItemAnlagenArten = new ChoiceActionItem(curLiegenschaft.Oid.ToString(), "-Anlagenarten-", curLiegenschaft);
                                mainItemAnlagenArten.ImageName = "gear_in_16";
                                mainItemAnlagenArten.BeginGroup = true;

                                IList<boAnlagenKategorie> lstAnlagenGruppen = os.GetObjects<boAnlagenKategorie>(new BinaryOperator("Aktiv", true, BinaryOperatorType.Equal));
                                //für jede Gruppe ein MainItem erstellen und dann die Arten dazu durchparsen

                                foreach (boAnlagenKategorie anlGruppe in lstAnlagenGruppen)
                                {
                                    ChoiceActionItem groupItem = new ChoiceActionItem(anlGruppe.Oid.ToString(), anlGruppe.Bezeichnung, curLiegenschaft);
                                    groupItem.ImageName = "interface_preferences";
                                    if (anlGruppe.lstAnlagenarten != null)
                                    {
                                        foreach (boAnlagenArt anlagenArt in anlGruppe.lstAnlagenarten)
                                        {
                                            ChoiceActionItem curAnlagenItem = new ChoiceActionItem(anlagenArt.Oid.ToString(), anlagenArt.Bezeichnung, anlagenArt);
                                            curAnlagenItem.ImageName = "gear_in_16";

                                            groupItem.Items.Add(curAnlagenItem);
                                        }
                                    }
                                    mainItemAnlagenArten.Items.Add(groupItem);

                                }
                                //

                                this.doAddAnlage.Items.Add(mainItemAnlagenArten);
                                #endregion
                            }
                        }
                        catch
                        {

                        }
                    }

                    break;
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        #region Anlagengruppe hinzufügen

        private void doAddHaustechnikKomponente_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            //die Haustechnikkomponente muss ja auch noch in die Liegenschaft integriert werden
            //IObjectSpace os = this.ObjectSpace;
            IObjectSpace workingOs = Application.CreateObjectSpace();
            DetailView curView = (DetailView)View;
            boLiegenschaft curLiegenschaft = workingOs.GetObjectByKey<boLiegenschaft>(((boLiegenschaft)curView.CurrentObject).Oid);
            
            //was wurde gewählt
            ChoiceActionItem chosenOption = e.SelectedChoiceActionItem;
            //die Haustechnikkomponente auch gleich erstellen
           
            var message = string.Empty;
            if (chosenOption.Data != null)
            {
                fiTechnikeinheit chosenUnit = (fiTechnikeinheit)e.SelectedChoiceActionItem.Data;
                fiTechnikeinheit workingUnit = workingOs.GetObjectByKey<fiTechnikeinheit>(chosenUnit.Oid);
                message = chosenUnit.SystemBezeichnung;

                //wird aktuell nicht benötigt
                //prüfen ob die Maximalanzahl überschritten wurde
               // fiTechnikDefinition relatedDefinition = workingOs.FindObject<fiTechnikDefinition>(new GroupOperator(new //BinaryOperator("FiObjekt.Objekttyp", curLiegenschaft.GetType(), BinaryOperatorType.Equal), new BinaryOperator("Anlagenart.Oid", chosenUnit.Basisanlage.Oid, BinaryOperatorType.Equal)));

                LgHaustechnikKomponente addedKomponente = workingOs.CreateObject<LgHaustechnikKomponente>();

                addedKomponente.Liegenschaft = curLiegenschaft;

                addedKomponente.Technikeinheit = workingUnit;
                //jetzt das Detailview dazu öffnen und die Werte anschreiben
                addedKomponente.Save();

                //hier brauch ich ein Popup das mir die Einstellung aufmacht, um die Bezeichnung zu setzen

                    //für jede zugeordnete Anlagengruppe eine Anlage anlegen
                    //ich brauch ja die Hauptanlage auch noch
                    //die Hauptanlage erstellen
                    boAnlage parentAnlage = workingOs.CreateObject<boAnlage>();
                    parentAnlage.AnlagenArt = workingOs.GetObjectByKey<boAnlagenArt>(chosenUnit.Basisanlage.Oid);
                    parentAnlage.Liegenschaft = curLiegenschaft;
                    
                   parentAnlage.Save();
                   addedKomponente.lstAnlagen.Add(parentAnlage);
                   addedKomponente.Save();
                    //os.CommitChanges();
                    if (chosenUnit.lstAnlagenarten != null)
                    {
                        foreach (boAnlagenArt item in workingUnit.lstAnlagenarten)
                        {
                            boAnlage subAnlage = workingOs.CreateObject<boAnlage>();
                            subAnlage.ParentAnlage = parentAnlage;
                            subAnlage.AnlagenArt = workingOs.GetObjectByKey<boAnlagenArt>(item.Oid);
                            subAnlage.Liegenschaft = curLiegenschaft;
                            subAnlage.Save();
                            addedKomponente.lstAnlagen.Add(subAnlage);
                            addedKomponente.Save();
                        }
                    }
                   
                DetailView dv = Application.CreateDetailView(workingOs, addedKomponente, true);
                e.ShowViewParameters.CreatedView = dv;            
                }
            refreshDetailView();
           // createMenue(doAddAnlage);
            }
        
        #endregion

        #region Anlage hinzufügen
        public void doAddAnlage_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            //IObjectSpace os = this.ObjectSpace;
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView curView = (DetailView)View;
            boLiegenschaft curLiegenschaft = os.GetObjectByKey<boLiegenschaft>(((boLiegenschaft)curView.CurrentObject).Oid);
            //allgemein -> Auswahl der Parameter und dann DetailView öffnen
            ChoiceActionItem currentSelection = e.SelectedChoiceActionItem;
            boAnlage mainUnit;
            if(currentSelection.ParentItem != null)
            {
                boAnlage result = null;
                boAnlagenArt selectedArt = os.GetObjectByKey<boAnlagenArt>(((boAnlagenArt)currentSelection.Data).Oid);
            if (currentSelection.ParentItem.Data.GetType() == typeof(LgHaustechnikKomponente))
            {
                //1. Anlage zu einer bestehenden Komponente hinzufügen
                LgHaustechnikKomponente curKomponente = os.GetObjectByKey<LgHaustechnikKomponente>(((LgHaustechnikKomponente)currentSelection.ParentItem.Data).Oid);
                    if (curKomponente != null)
                    {
                        //gibt es eine Hauptanlage?
                        mainUnit = curKomponente.lstAnlagen.Where(t => t.ParentAnlage == null).FirstOrDefault();
                        if (mainUnit != null)
                        {
                            //welche Anlagenart?
                            result = os.CreateObject<boAnlage>();
                            result.Liegenschaft = curLiegenschaft;
                            result.HaustechnikKomponente = curKomponente;
                            result.ParentAnlage = os.GetObjectByKey<boAnlage>(mainUnit.Oid);
                        }
                        //die Hauptanlage der Komponente finden
                   
                result.AnlagenArt = selectedArt;
                    }
            }

            //2. Anlage zur Liegenschaft hinzufügen
            //Wenn ein Item gewählt wurde das als Data die Gruppe hat:

           if(currentSelection.ParentItem.Data.GetType()== typeof(boLiegenschaft))
            {
                result = os.CreateObject<boAnlage>();
                result.Liegenschaft = curLiegenschaft;
                    if (currentSelection.Data.GetType() == typeof(boAnlagenKategorie))
                    {
                        result.AnlagenKategorie = os.GetObjectByKey<boAnlagenKategorie>(((boAnlagenKategorie)currentSelection.Data).Oid);
                    }
                    else
                    {
                        result.AnlagenArt = selectedArt;
                    }
                
              
            }
           if(result != null)
            {
                DetailView dv = Application.CreateDetailView(os, result);
                e.ShowViewParameters.CreatedView = dv;

            }
            }
        }
        #endregion

        
        private void refreshDetailView()
        {
            DetailView curView = (DetailView)View;


            boLiegenschaft curLiegenschaft = this.ObjectSpace.GetObjectByKey<boLiegenschaft>(((boLiegenschaft)curView.CurrentObject).Oid);
            curLiegenschaft.Reload();
            this.ObjectSpace.ReloadObject(curLiegenschaft);
            curLiegenschaft.Anlagen.Reload();
            curLiegenschaft.lstHaustechnikKomponenten.Reload();
            this.View.ObjectSpace.ReloadObject(curLiegenschaft);
            this.View.ObjectSpace.Refresh();
            curView.Refresh();
            createMenue(this.doAddAnlage);
            createMenue(this.doAddHaustechnikKomponente);
            createMenue(this.doDeleteAnlagenGruppe);
        }

        private void doDeleteAnlagenGruppe_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace workingOs = Application.CreateObjectSpace();
            ChoiceActionItem chosenOption = e.SelectedChoiceActionItem;
            LgHaustechnikKomponente chosenKomponente;
            
            if(chosenOption.Data != null)
            {
                //hier ist die Technikeinheit drin

                chosenKomponente = workingOs.GetObjectByKey<LgHaustechnikKomponente>(((LgHaustechnikKomponente)chosenOption.Data).Oid);
                workingOs.Delete(chosenKomponente);
                workingOs.CommitChanges();
                refreshDetailView();
            }

        }
    }
}
