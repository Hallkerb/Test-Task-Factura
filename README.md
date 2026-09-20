# Test Task Factura: Unity Developer

Core-механіка гри: авто з туреллю їде рівнем, гравець керує напрямком стрільби, вороги біжать в атаку при наближенні авто.

## Запуск

- **Версія Unity:** 6000.3.8f1
- **Стартова сцена:** `SampleScene`
- **Час виконання:** приблизно 20 годин

## Керування

- **Тап по екрану** ‒ старт гри та перезапуск рівня.
- **Рух курсору / пальця** ‒ керування напрямком турелі.
- Стрільба автоматична.

## Геймплей

1. Тап ‒ авто їде вперед, камера слідує за ним.
2. Вороги стоять у idle. Коли авто наближається, вони біжать до нього й завдають шкоди.
3. Перемога ‒ авто доїхало до фінішу. Поразка ‒ HP авто закінчилось.
4. Тап ‒ рівень починається спочатку, авто стоїть до наступного тапу.

## Структура скриптів

```
Scripts/
├── Core/          GameManager, LevelManager, SpawnManager, Health, HitFeedback
├── Enums/         SpawnableType, Team
├── Gameplay/
│   ├── Camera/    CameraController
│   ├── Cars/      рух, турель, куля, ефекти авто
│   ├── Enemies/   Stickman: рух, анімація, атака
│   └── Environment/  FinishLine
├── Interfaces/    IHealth, ISpawnable, ITeamMember, IAttackable
└── UI/            UIManager, UIScreens, UIHealth, UIHealthPool, UILevelProgress
```

## Архітектурні рішення

- **Компонентний підхід.** Авто складається з окремих компонентів з однією відповідальністю: `CarMovement` (рух), `CarTurret` (стрільба), `PlayerTurretController` (введення), `Health`, `HitFeedback`, `CarEffectsController`. Так само ворог: `StickmanMovement` і `StickmanAnimatorController`.
- **Інтерфейси замість прямих залежностей.** `IHealth`, `ISpawnable`, `ITeamMember`, `IAttackable`. Куля не знає, у кого влучає: вона працює з `IHealth` і перевіряє `Team`.
- **Пул об'єктів.** `SpawnManager` перевикористовує авто, ворогів і кулі за допомогою `SpawnableType`, `DespawnAll()` викликається при рестарті. Health-бари теж у власному пулі (`UIHealthPool`).
- **Події для зв'язку.** `GameManager` розсилає `OnStart / OnEnd / OnWin / OnLose / OnRestart`, компоненти підписуються й відписуються самі.
- **Розширюваність.** Новий ворог: префаб на базі `BaseEnemy` з реалізацією `IAttackable`, значення в `SpawnableType` і елемент у списку `poolItems` компонента `SpawnManager` (в інспекторі). Новий тип авто додається так само через `BaseCar`. Існуючий код не змінюється.

### Свідомі компроміси

- Прості статичні синглтони замість DI-контейнера: для core-механіки це менше коду, а залежності добре видно.
- Корутини для покадрових анімацій UI (async там не дає переваг).

### Що покращив би далі

- Перейти на VContainer, а цикл гри переписати на UniTask.
- Єдиний стан гри (`Idle / Playing / Ended`) замість набору подій.
