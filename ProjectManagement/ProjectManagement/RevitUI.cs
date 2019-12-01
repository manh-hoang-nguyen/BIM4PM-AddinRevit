using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjectManagement
{
    class Onglet
    {


        public void Ajouter(UIControlledApplication application, string nomOnglet)
        {
            application.CreateRibbonTab(nomOnglet);
        }

    }
    class Ruban
    {

        public RibbonPanel Ajouter(UIControlledApplication application, string NomOnglet, string NomRuban)
        {
            return application.CreateRibbonPanel(NomOnglet, NomRuban);
        }
    }
    class Bouton
    {
        // Les constructeurs

        // Les méthodes
        /// <summary>
        /// Ajoute un bouton dans le complément RZB.
        /// </summary>
        /// <param name="NomPanneau"></param>
        /// <param name="NomBouton"></param>
        /// <param name="NomImage"></param>
        /// <param name="NomClass"></param>
        /// <param name="CheminAssembleur"></param>
        /// <param name="CommentaireBtn"></param>
        public void Ajouter(RibbonPanel NomPanneau, string NomBouton, string NomImage, string NomClass,
                            string CheminAssembleur, string CommentaireBtn)
        {
            // Créer un bouton dans le panneau
            PushButtonData b1Data = new PushButtonData("cmd" + NomBouton, NomBouton + System.Environment.NewLine,
                                                        CheminAssembleur, NomClass);

            PushButton pb1 = NomPanneau.AddItem(b1Data) as PushButton;
            pb1.ToolTip = CommentaireBtn;
            ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url, "http://www.autodesk.com");
            pb1.SetContextualHelp(contextHelp);

            // Recherche de l'image du bouton dans les ressources.
            pb1.LargeImage = OutilImage.ImageSource("TEST.Resources." + NomImage);
        }
    }

    class Separateur
    {
        // Les champs

        // Les accesseurs ou propriétés

        // Les constructeurs.

        // Les méthodes

        /// <summary>
        /// Crée un séparateur
        /// </summary>
        /// <param name="NomPanneau"></param>
        public static void Ajouter(RibbonPanel NomPanneau)
        {
            NomPanneau.AddSeparator();
        }

    }
    class OutilImage
    {
        /// <summary>
        /// Récupère une image (png, ico, jpeg, bmp) dans les ressources.
        /// </summary>
        /// <param name="nomImage"></param>
        /// <param name="objet"></param>
        /// <returns></returns>
        public static ImageSource ImageSource(string nomImage)
        {

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(nomImage);

            if (Path.GetExtension(nomImage).ToLower().EndsWith(".png")) // Cas png
            {
                PngBitmapDecoder img = new System.Windows.Media.Imaging.PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                return img.Frames[0];
            }

            if (Path.GetExtension(nomImage).ToLower().EndsWith(".bmp")) // Cas bmp
            {
                BmpBitmapDecoder img = new System.Windows.Media.Imaging.BmpBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                return img.Frames[0];
            }

            if (Path.GetExtension(nomImage).ToLower().EndsWith(".jpg")) // Cas jpg
            {
                JpegBitmapDecoder img = new System.Windows.Media.Imaging.JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                return img.Frames[0];
            }

            if (Path.GetExtension(nomImage).ToLower().EndsWith(".tiff")) // Cas tiff
            {
                TiffBitmapDecoder img = new System.Windows.Media.Imaging.TiffBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                return img.Frames[0];
            }

            if (Path.GetExtension(nomImage).ToLower().Contains(".ico")) // Cas  ico
            {
                IconBitmapDecoder img = new System.Windows.Media.Imaging.IconBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                return img.Frames[0];
            }

            return null;
        }
    }
}
