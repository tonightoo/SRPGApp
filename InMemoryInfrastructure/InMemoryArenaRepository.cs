using Domain.Models;
using UseCase.Repositories;
using UseCase.State;
using System.Collections.Generic;

namespace InMemoryInfrastructure
{
    public class InMemoryArenaRepository
        : IArenaRepository
    {

        private Arena arena;

        public Arena InitializeById(int id)
        {
            List<List<Square>> squares = new List<List<Square>>();
            for (int i = 0; i < 10; i++)
            {
                List<Square> row = new List<Square>();
                for (int j = 0; j < 10; j++)
                {
                    row.Add(new Square(1, 1));
                }
                squares.Add(row);
            }

            squares[2][2].Unit = Unit.Builder.SetName("unit1")
                                             .SetImageId(0)
                                             .SetMaxHpAndHp(50)
                                             .SetMaxMpAndMp(10)
                                             .SetAttack(20)
                                             .SetDeffece(8)
                                             .SetMagicAttack(5)
                                             .SetMagicDeffence(5)
                                             .SetTechnic(5)
                                             .SetLuck(5)
                                             .SetStep(3)
                                             .SetTeamId(1)
                                             .SetWeapon(Weapon.WeaponType.Sword)
                                             .Build();

            squares[6][5].Unit = Unit.Builder.SetName("unit3")
                                             .SetImageId(6)
                                             .SetMaxHpAndHp(40)
                                             .SetMaxMpAndMp(10)
                                             .SetAttack(10)
                                             .SetDeffece(4)
                                             .SetMagicAttack(5)
                                             .SetMagicDeffence(5)
                                             .SetTechnic(5)
                                             .SetLuck(5)
                                             .SetStep(4)
                                             .SetTeamId(1)
                                             .SetWeapon(Weapon.WeaponType.Bow)
                                             .Build();


            squares[8][8].Unit = Unit.Builder.SetName("unit2")
                                             .SetImageId(3)
                                             .SetMaxHpAndHp(10)
                                             .SetMaxMpAndMp(10)
                                             .SetAttack(15)
                                             .SetDeffece(6)
                                             .SetMagicAttack(10)
                                             .SetMagicDeffence(10)
                                             .SetTechnic(10)
                                             .SetLuck(10)
                                             .SetStep(4)
                                             .SetTeamId(2)
                                             .SetWeapon(Weapon.WeaponType.Spear)
                                             .Build();

            squares[8][9].Unit = Unit.Builder.SetName("unit4")
                                             .SetImageId(9)
                                             .SetMaxHpAndHp(40)
                                             .SetMaxMpAndMp(10)
                                             .SetAttack(10)
                                             .SetDeffece(4)
                                             .SetMagicAttack(10)
                                             .SetMagicDeffence(10)
                                             .SetTechnic(10)
                                             .SetLuck(10)
                                             .SetStep(3)
                                             .SetTeamId(2)
                                             .SetWeapon(Weapon.WeaponType.Bow)
                                             .Build();


            Map map = new Map(squares);

            arena = new Arena(map, new SelectUnitState());
            arena.commandPanel = new CommandPanel();

            return arena;
        }

        public void Save(Arena arena)
        {
            this.arena = arena;
        }
        public Arena Load()
        {
            return this.arena;
        }

    }
}
