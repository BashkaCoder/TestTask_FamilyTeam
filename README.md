# Душа Ассасина

## 📌 Описание проекта
**Душа Ассасина** — динамичный экшен с элементами платформера и шутера, где игроку предстоит управлять героем, чья душа была изгнана из тела и перенесена в другой мир. Используя свои навыки ближнего и дальнего боя, персонаж должен вернуться в свою реальность и одолеть антагониста.

## 🎮 Геймплей

- **2D-сцена:** классический платформер с ближним боем.
- **3D-сцена:** бои с дальними атаками и сложной механикой насыщения + усиления противников.
- **Интерактивные элементы:** двери, телепорты, ловушки.

## 🛠️ Техническая реализация

### 📂 Архитектура кода
Проект разделен на логические модули:

- **Core** — управление игровым процессом, уровни, UI.
- **2D** — скрипты для платформера.
- **3D** — скрипты для трёхмерной части игры.
- **Combat** — боевые механики.
- **Movement** — системы передвижения.

### 🔗 Связи между компонентами
```plantuml
@startuml

package "Game" {
    package "Core" {
        class GameManager {
            + StartGame()
            + EndGame()
        }
        class LevelManager {
            + LoadLevel()
        }
    }

    package "2D" {
        package "Player" {
            class PlayerController {
                + Move()
                + Attack()
            }
            class PlayerHealth {
                + TakeDamage()
                + Heal()
            }
        }
        package "Enemy" {
            class Enemy {
                + Move()
                + Attack()
            }
            class EnemyHealth {
                + TakeDamage()
            }
        }
        package "Environment" {
            class Door {
                + Open()
            }
        }
        package "Combat" {
            class Weapon {
                + Fire()
            }
        }
    }

    package "3D" {
        package "Control" {
            class EnemyZone {
                + Activate()
            }
            class EnemyZoneManager {
                + ManageZones()
            }
        }
        package "Movement" {
            class Teleport {
                + TeleportPlayer()
            }
        }
        package "UI" {
            class ZoneView {
                + DisplayZoneInfo()
            }
        }
        class CameraFacing {
            + UpdateOrientation()
        }
        class DestroyAfterEffect {
            + DestroyObject()
        }
        class EnemyType {
            + DefineType()
        }
    }

    GameManager --> LevelManager
    GameManager --> UIManager
    LevelManager --> "2D.Player.PlayerController"
    "2D.Player.PlayerController" --> "2D.Player.PlayerHealth"
    "2D.Player.PlayerController" --> "2D.Combat.Weapon"
    "2D.Player.PlayerController" --> "2D.Environment.Door"
    "2D.Enemy.Enemy" --> "2D.Enemy.EnemyHealth"
    "2D.Player.PlayerController" --> "2D.Enemy.Enemy" : "Атакует"
    "2D.Enemy.Enemy" --> "2D.Player.PlayerController" : "Атакует"
    "2D.Environment.Door" --> LevelManager : "Меняет сцену"
    "3D.Control.EnemyZoneManager" --> "3D.Control.EnemyZone"
    "3D.Control.EnemyZone" --> "3D.UI.ZoneView"
    "3D.Movement.Teleport" --> LevelManager : "Меняет сцену"
    "3D.CameraFacing" --> "3D.UI.ZoneView"
    "3D.DestroyAfterEffect" --> "3D.EnemyType"

}
@enduml
```

![Архитектура](https://github.com/user-attachments/assets/f32a0361-d8f5-4398-aa3b-9c336eaf1f0d)

🚀 Запуск проекта

1. **Клонируйте репозиторий:**
  ```bash
   git clone https://github.com/BashkaCoder/TestTask_FamilyTeam.git
  ```

2. **Откройте проект в Unity (версия 6.0000.34f1 и выше).**
3. **Запустите сцену `2D.unity` в редакторе.**

## 📸 Скриншоты
Скриншот_1 ![1](https://github.com/user-attachments/assets/e0c79898-6366-4f72-a5d5-e81b85882603)
Скриншот_2 ![2](https://github.com/user-attachments/assets/28995488-aacf-4e79-a1d3-9a44dbe6fdc2)

## 📌 Контакты
- **Автор:** [Башинский Д.О.](https://t.me/dbashinskiy)
- **Почта:** dbashinskiy@miem.hse.ru

## 📜 Лицензия
Этот проект распространяется под лицензией MIT. Свободно используйте, изменяйте и дополняйте ;)
