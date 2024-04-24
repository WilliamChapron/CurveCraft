using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathProjet
{
    public partial class Form1 : Form
    {
        private PlotView plotView;
        private PlotModel plotModel;
        private LineSeries series;

        private Timer timer;
        private double time = 0;
        private double dt = 0.1; // Intervalle de temps entre les mises à jour (en secondes)

        public Form1()
        {
            InitializeComponent();
            InitializePlot();
            InitializeTimer();
        }

        private void InitializePlot()
        {
            // Créer le modèle de plot
            plotModel = new PlotModel { Title = "Courbe dynamique en fonction du temps" };

            // Ajouter une série de données (initialisée avec une valeur nulle)
            series = new LineSeries();
            plotModel.Series.Add(series);

            // Créer le contrôle PlotView et l'ajouter au formulaire
            plotView = new PlotView
            {
                Dock = DockStyle.Fill,
                Model = plotModel
            };
            Controls.Add(plotView);
        }

        private void InitializeTimer()
        {
            // Initialiser la minuterie avec l'intervalle de temps spécifié
            timer = new Timer();
            timer.Interval = (int)(dt * 1000); // Convertir dt en millisecondes
            timer.Tick += Timer_Tick;
            timer.Start(); // Démarrer la minuterie
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Mettre à jour les données de la série avec de nouvelles valeurs
            double y = Math.Sin(time); // Par exemple, une fonction sinus pour simuler une évolution dans le temps
            series.Points.Add(new DataPoint(time, y));

            // Incrémenter le temps
            time += dt;

            // Limiter le nombre de points affichés pour éviter les fuites de mémoire
            if (series.Points.Count > 100)
            {
                series.Points.RemoveAt(0); // Supprimer le premier point
            }

            // Mettre à jour le graphique avec les nouvelles données
            plotView.InvalidatePlot(true);
        }



        private void start_App_btn_Click(object sender, EventArgs e)
        {
            // Récupérer la position de Form1
            Point form1Location = this.Location;

            // Cacher Form1
            this.Hide();

            // Créer et configurer Form2
            Form2 form2 = new Form2();
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = form1Location;

            // Montrer Form2 avec une animation
            form2.Opacity = 1; // Définir l'opacité à 0 pour commencer
            form2.Show();

            // Ajouter une animation pour augmenter l'opacité en douceur
            //Timer timer = new Timer();
            //timer.Interval = 20; // Interval en millisecondes
            //timer.Tick += (timerSender, args) =>
            //{
            //    form2.Opacity += 0.05; // Augmenter l'opacité progressivement
            //    if (form2.Opacity >= 1) // Arrêter l'animation lorsque l'opacité atteint 1
            //    {
            //        timer.Stop(); // Arrêter le timer
            //    }
            //};
            //timer.Start(); // Démarrer le timer
        }
    }
}
