# Go-Down-Stairs

# 3D 小朋友下樓梯

### 3D Endless Descent Platform Survival Game

**Unity / C# / Mobile / AI-assisted Development**

一款以「持續下墜」為核心玩法的 3D 平台生存遊戲。

玩家需要控制角色在不斷向上移動的平台間向下移動，尋找下一個可站立的平台，同時躲避尖刺、碎裂平台與其他特殊平台，並維持生命值。

本專案從 Unity 基礎角色控制開始，逐步建立平台生成、物件池、生命系統、難度控制、UI、動畫與手機觸控操作等系統。

---

## Gameplay

玩家會持續向下移動，平台則持續向上，遊戲過程中會出現不同種類的平台，玩家需要在有限的生命值與持續增加的遊戲壓力下，盡可能抵達更深的樓層。

---

## Core Features

### Player Controller

* 第三人稱 3D 移動
* 跳躍與快速下落
* 相對鏡頭方向移動
* 角色移動與動畫切換
* 跑步、跳躍、掉落動畫

### Platform System

以 `PlatformBase` 作為平台基礎類別，將不同平台效果獨立實作。

```text
PlatformBase
├── NormalPlatform
├── SpikePlatform
├── HealPlatform
├── BreakPlatform
├── SpeedPlatform
├── SlowPlatform
├── ConveyorPlatform
└── BouncePlatform
```

這樣可以在不修改玩家主要移動邏輯的情況下，持續增加新的平台種類。

### Procedural Platform Generation

遊戲以「層」為單位管理生成。

目前設定為每 10 個平台為一層，並依樓層建立平台配置表，再透過 Shuffle 打亂生成順序。

例如：

```text
Normal × 10
Spike × 4
Heal × 2
Break × 4
```

這種方式相比完全隨機生成，可以控制不同平台的比例，同時保留隨機性與遊戲變化。

### Object Pooling

平台會持續生成與回收，因此使用 `PoolManager` 管理不同種類的平台物件池。

```text
PoolManager
├── Normal Pool
├── Heal Pool
├── Spike Pool
└── Break Pool
```

減少 Endless 關卡中頻繁建立與銷毀物件造成的效能負擔。

### Health & Feedback

* 玩家生命值
* 尖刺持續傷害
* 回血平台
* 掉出世界死亡
* 頂部危險區
* 受傷 Camera Shake
* 延遲血條

### Depth UI

使用全長位置圖呈現玩家目前在整體關卡中的高度 / 深度位置，並搭配樓層 UI 讓玩家了解目前進度。

### Mobile Controls

針對手機重新設計操作方式：

```text
左半部
└── Floating Joystick
    └── 移動

右半部
├── Tap
│   └── Jump
└── Drag
    └── Camera Rotation
```

同時處理左右觸控輸入互相干擾的問題，使左側搖桿拖曳時不會誤觸發鏡頭旋轉。

---

## Architecture

```text
GameManager
│
├── RoundManager
├── PlatformManager
│   └── PoolManager
│
├── Player
│   ├── PlayerMovementManager
│   ├── PlayerEffectManager
│   └── Health System
│
├── CameraManager
│
└── UI System
```

玩家效果另外由 `PlayerEffectManager` 管理，例如：

```text
SpeedBoost
Slow
```

每個效果包含倍率與持續時間，使平台效果與玩家移動邏輯分離。

---

## Development Process

本專案採用 Bottom-up 的方式開發，從最基礎的角色控制逐步建立完整遊戲系統。

### Phase 1 — Core Controller

**5/22**

* Player
* PlayerMovementManager
* 移動
* 跳躍
* 跑步
* CameraManager
* 鏡頭跟隨
* GameManager

### Phase 2 — Platform System

**5/22–5/24**

* 平台生成
* 平台站立判定
* 平台向上移動
* 平台回收
* Object Pool
* 不同平台種類
* 尖刺
* 回血
* 碎裂平台

### Phase 3 — Game Loop

**5/23–6/6**

* 生命系統
* 掉出世界死亡
* 頂部尖刺 / 邊界
* 每 10 個平台一層
* 平台配置表
* 血量 UI
* 層數 UI
* 開始介面
* 結束介面
* 遊戲重置

### Phase 4 — Gameplay Feedback

**6/3–6/13**

* 延遲血條
* Camera Shake
* 受傷 / 回血視覺效果
* 全長 Y 座標圖
* 影子落點提示
* 玩家模型
* 玩家動畫
* 跑步 / 跳躍 / 掉落動畫

### Phase 5 — Mobile Control

**6/5**

* Floating Joystick
* 右半部點擊跳躍
* 右半部拖曳鏡頭
* 左右觸控區域分離
* 手機操作調整

### Phase 6 — Special Platforms

**6/9–6/13**

* 加速平台
* 減速平台
* 彈跳平台
* 傳送帶
* 易滑平台
* PlayerEffectManager
* 玩家速度倍率與效果持續時間

---

## Problems & Solutions

### 平台移動速度提高後產生穿模

提高遊戲節奏時發現平台偶爾會穿過玩家。

針對平台移動方式、Rigidbody、物理更新與碰撞偵測進行調整，避免速度提高後產生不穩定碰撞。

### 手機搖桿拖曳造成鏡頭旋轉

手機端使用虛擬搖桿後，發現左側拖曳會同時觸發鏡頭輸入。

後續將輸入區域分離：

```text
Left → Movement
Right Tap → Jump
Right Drag → Camera
```

解決不同觸控輸入互相干擾的問題。

### 隨機平台可能產生不合理配置

完全獨立隨機的平台可能造成危險平台連續出現，甚至形成不合理路線。

因此改用「平台配置表 + Shuffle」控制隨機性，讓平台種類比例可以被設計與調整。

---

## AI-assisted Development

本專案使用 AI 作為開發輔助工具，主要協助：

* Unity / C# 程式架構討論
* 功能拆解
* Bug 分析
* 程式草稿
* 系統設計討論
* 手機操作設計

AI 產生的程式與建議並非直接作為最終成果，而是經過本人在 Unity 中整合、測試、修改與除錯。

透過 AI 輔助，本專案也嘗試建立自己對物件導向、元件化設計、事件系統、Object Pooling 與程序生成的理解。

---

## Learning Outcomes

透過本專案，我主要學習了：

* Unity 3D 角色控制
* Rigidbody 與物理系統
* Camera 與第三人稱視角
* C# 物件導向設計
* 基底類別與多型
* Object Pooling
* 程序化平台生成
* 遊戲狀態與 Manager 架構
* UI 與遊戲狀態同步
* 手機觸控輸入
* Unity 動畫狀態控制
* Debug 與遊戲手感調整

其中最大的收穫並不是完成單一功能，而是學習如何從一個簡單的 Prototype，逐步拆分問題並建立可以持續擴充的遊戲架構。

---

## Project Status

目前專案已完成核心玩法、平台系統、生命系統、UI、玩家動畫與手機操作 Prototype。

後續預計繼續改善：

* 程序生成的可玩路徑保證
* 難度曲線
* 更多特殊平台
* 音效與粒子效果
* 效能優化
* 完整手機版本

````
