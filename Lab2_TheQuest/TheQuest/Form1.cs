using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TheQuest.CommonConstants;

namespace TheQuest
{
    public partial class Form1 : Form
    {
        private static readonly IEnumerable<Keys> MOVES = new List<Keys>() { Keys.Up, Keys.Down, Keys.Left, Keys.Right };
        private static readonly IEnumerable<Keys> ATTACKS = new List<Keys>() { Keys.W, Keys.S, Keys.A, Keys.D };

        private Game game;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            game = new Game(new Rectangle(BOUNDARY_X, BOUNDARY_Y, BOUNDARY_WIDTH, BOUNDARY_HEIGHT), random);
            game.NewLevel();
            player.Visible = true;
            UpdateCharacters();
        }

        private void UpdateCharacters()
        {
            player.Location = game.PlayerLocation;
            playerHitPoints.Text = game.PlayerHitPoints.ToString();

            foreach (var weapon in game.PlayerWeapons)
            {
                switch (weapon)
                {
                    case Weapons.Sword:
                        inventorySword.Visible = true;
                        break;
                    case Weapons.BluePotion:
                        inventoryBluePotion.Visible = true;
                        break;
                    case Weapons.Bow:
                        inventorySword.Visible = true;
                        break;
                    case Weapons.RedPotion:
                        inventoryBluePotion.Visible = true;
                        break;
                    case Weapons.Mace:
                        inventorySword.Visible = true;
                        break;
                }
            }

            var showBat = false;
            var showGhost = false;
            var showGhoul = false;
            var enemiesShown = 0;

            foreach (var enemy in game.Enemies)
            {
                if (enemy is Bat)
                {
                    bat.Location = enemy.Location;
                    batHitPoints.Text = enemy.HitPoints.ToString();
                    if (!enemy.Dead)
                    {
                        showBat = true;
                        enemiesShown++;
                    }
                }
            }

            bat.Visible = showBat;
            ghost.Visible = showGhost;
            ghoul.Visible = showGhoul;

            sword.Visible = false;
            bluePotion.Visible = false;
            bow.Visible = false;
            redPotion.Visible = false;
            mace.Visible = false;

            var weaponControl = DetermineWeaponControl(game.WeaponInRoom);
            weaponControl.Visible = true;

            weaponControl.Location = game.WeaponInRoom.Location;
            if (game.WeaponInRoom.PickedUp) { weaponControl.Visible = false; }
            else { weaponControl.Visible = true; }

            if (game.PlayerHitPoints <= 0)
            {
                MessageBox.Show("You died");
                Application.Exit();
            }

            if (enemiesShown < 1)
            {
                MessageBox.Show("You have defeated the enemies on this level");
                game.NewLevel();
                UpdateCharacters();
            }
        }

        private Control DetermineWeaponControl(Weapon weapon)
        {
            if (weapon.Name == Weapons.Sword) { return sword; }
            if (weapon.Name == Weapons.BluePotion) { return bluePotion; }
            if (weapon.Name == Weapons.Bow) { return bow; }
            if (weapon.Name == Weapons.RedPotion) { return redPotion; }
            return mace;
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var key = e.KeyCode;

            if (IsMove(key))
            {
                game.Move(DetermineMoveDirection(e.KeyCode));
                UpdateCharacters();
            }
            if (IsAttack(key))
            {
                game.Attack(DetermineAttackDirection(key));
                UpdateCharacters();
            }
        }

        private static Keys DetermineKeyPressed(KeyPressEventArgs e)
        {
            return (Keys)Enum.Parse(typeof(Keys), e.KeyChar.ToString());
        }

        private bool IsAttack(Keys key)
        {
            return ATTACKS.Contains(key);
        }

        private bool IsMove(Keys key)
        {
            return MOVES.Contains(key);
        }

        private Direction DetermineMoveDirection(Keys key)
        {
            if (key == Keys.Up) { return Direction.Up; }
            if (key == Keys.Down) { return Direction.Down; }
            if (key == Keys.Left) { return Direction.Left; }
            if (key == Keys.Right) { return Direction.Right; }
            throw new InvalidEnumArgumentException();
        }

        private Direction DetermineAttackDirection(Keys key)
        {
            if (key == Keys.A) { return Direction.Left; }
            if (key == Keys.S) { return Direction.Down; }
            if (key == Keys.D) { return Direction.Right; }
            if (key == Keys.W) { return Direction.Up; }
            throw new InvalidEnumArgumentException();
        }

        private void inventorySword_Click(object sender, EventArgs e)
        {
            MaybeEquipWeapon(Weapons.Sword, inventorySword);
        }

        private void inventoryBluePotion_Click(object sender, EventArgs e)
        {
            MaybeEquipWeapon(Weapons.BluePotion, inventoryBluePotion);
        }

        private void inventoryBow_Click(object sender, EventArgs e)
        {
            MaybeEquipWeapon(Weapons.Bow, inventoryBow);
        }

        private void inventoryRedPotion_Click(object sender, EventArgs e)
        {
            MaybeEquipWeapon(Weapons.RedPotion, inventoryRedPotion);
        }

        private void inventoryMace_Click(object sender, EventArgs e)
        {
            MaybeEquipWeapon(Weapons.Mace, inventoryMace);
        }

        private void MaybeEquipWeapon(Weapons weaponName, PictureBox inventoryItem)
        {
            if (!game.CheckPlayerInventory(weaponName)) { return; }
            game.Equip(weaponName);
            UpdateInventoryBorders(inventoryItem);
        }

        private void UpdateInventoryBorders(PictureBox inventoryItem)
        {
            inventorySword.BorderStyle = BorderStyle.None;
            inventoryBluePotion.BorderStyle = BorderStyle.None;
            inventoryBow.BorderStyle = BorderStyle.None;
            inventoryRedPotion.BorderStyle = BorderStyle.None;
            inventoryMace.BorderStyle = BorderStyle.None;

            inventoryItem.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
