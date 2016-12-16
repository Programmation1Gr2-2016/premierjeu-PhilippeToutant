using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteAnime
{
    class GameObjectAnimé
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Vector2 direction;
        public Rectangle position;
        public Rectangle spriteAfficher;
        public enum etats { attenteDroite, attenteGauche, runDroite, runGauche, runBas, attenteHaut, runHaut, attenteBas };
        public etats objetState;
        private int cpt = 0;

        int runState = 0; //État de départ
        int nbEtatRun = 3; //Combien il y a de rectangles pour l’état “courrir”
        public Rectangle[] tabRunDroite = {
            new Rectangle(263, 90, 30, 43),
            new Rectangle(293, 90, 30, 43),
            new Rectangle(323, 90, 30, 43),
                                          };
        public Rectangle[] tabRunGauche = {
            new Rectangle(264, 45, 30, 43),
            new Rectangle(294, 45, 30, 43),
            new Rectangle(324, 45, 30, 43),
                                          };
        public Rectangle[] tabRunBas =    {
            new Rectangle(263, 1, 30, 43),
            new Rectangle(293, 1, 30, 43),
            new Rectangle(322, 1, 30, 43),
                                          };
        public Rectangle[] tabRunHaut = {
            new Rectangle(263, 133, 30, 43),
            new Rectangle(293, 133, 30, 43),
            new Rectangle(322, 133, 30, 43),
                                           };
        int waitState = 0;
        public Rectangle[] tabAttenteDroite =
        {
            new Rectangle(293, 90, 30, 43)
        };


        public Rectangle[] tabAttenteGauche =
        {
            new Rectangle(294, 45, 30, 43)
        };

        public Rectangle[] tabAttenteHaut =
       {
            new Rectangle(293, 133, 30, 43)
        };

        public Rectangle[] tabAttenteBas =
       {
            new Rectangle(293, 1, 30, 43)
        };

        public virtual void Update(GameTime gameTime)
        {
            if (objetState == etats.attenteDroite)
            {
                spriteAfficher = tabAttenteDroite[waitState];
            }
            if (objetState == etats.attenteGauche)
            {
                spriteAfficher = tabAttenteGauche[waitState];
            }
            if (objetState == etats.runDroite)
            {
                spriteAfficher = tabRunDroite[runState];
            }
            if (objetState == etats.runGauche)
            {
                spriteAfficher = tabRunGauche[runState];
            }
            if (objetState == etats.runHaut)
            {
                spriteAfficher = tabRunHaut[runState];
            }
            if (objetState == etats.runBas)
            {
                spriteAfficher = tabRunBas[runState];
            }

            // Compteur permettant de gérer le changement d'images
            cpt++;
            if (cpt == 6) //Vitesse défilement
            {
                //Gestion de la course
                runState++;
                if (runState == nbEtatRun)
                {
                    runState = 0;
                }
                cpt = 0;
            }
        }

    }
}
