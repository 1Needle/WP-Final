# WP-Final

<主角>
動作:
移動:
  WASD移動，LShift+WASD奔跑，Space跳躍
主角滑鼠左鍵攻擊:
  有1秒CD，造成少量傷害，只能在行走及站立時攻擊
主角滑鼠右鍵攻擊:
  舉盾防禦，期間不會受到傷害，但只能站立不動

技能:
鍵盤左上數字鍵1(alpha1):
  提升攻擊力，打敵人會使著火
鍵盤左上數字鍵2(alpha2):
  持續回血
鍵盤左上數字鍵3(alpha3):
  爆炸並造成範圍傷害，砍死敵人會減CD

<敵人>
*以下為有實作的功能，但並未全數應用在遊戲中
    1.骷髏
        閒置狀態:
            -可設置任意數量的巡邏點，沿指定路徑巡邏，到定點後會播放動畫
            -玩家進入偵測範圍後進入戰鬥狀態
            -移動方式為走路
        戰鬥狀態:
            -進入戰鬥狀態時進行戰吼提示玩家
            -會追擊玩家
            -移動方式為跑步
            -玩家進入攻擊範圍會停下並攻擊
            -離開仇恨範圍、離重生點太遠，都會進入追丟狀態
            -有擊中特效、受傷死亡動畫
        追丟狀態:
            -播放動畫，此段時間內不能移動
            -如果玩家太靠近會中斷動畫直接攻擊，是唯一中斷動畫的方法
            -動畫結束後會返回原先的巡邏路徑
    2.Boss
        部位傷害:
            -身體每個部位都有獨立的Collider，根據攻擊部位會增減攻擊力
        閒置狀態:
            -玩家遠離時會睡覺
            -玩家靠近時會爬起來警戒玩家
            -再靠近會進入戰鬥狀態
        對峙狀態:
            -平時為對峙狀態，會保持距離繞著玩家遊走
            -與玩家拉開距離，增加動作的多樣性
            -每次進行完攻擊都會拉開距離回到對峙狀態
            -若被追逐，有機率使用傳送回到玩家背後
        戰鬥狀態:
            -近戰攻擊:Basic attack, Claw attack
            -遠程攻擊:Flame attack, Magic attack
            -有1個combo，Claw attack必定會銜接Basic attack
            -若玩家靠近會使用近戰攻擊，玩家遠離會使用遠程攻擊
            -如果玩家保持不動，一段時間後會隨機使用攻擊
            -Flame會使玩家著火，Magic會使玩家暈眩

<地圖&其他>
勝利條件: 擊殺Boss (最後房間的龍)
失敗條件: 玩家死亡 (hp < 0)

---/* 地圖設計理念參考 SkyBlock Dungeon F7 */---

三道門開啟條件
  第一道: 與NPC對話
  第二道: 擊殺5隻小怪
  第三道: 擊殺60隻小怪且啟動全部4根拉桿
  一二都和NPC搭配控制門開啟 

拉桿(四根)
  一定範圍內才能使用
  拉下永久啟動

  第一根拉桿跑酷抵達
  二三四根在平地
  原本還要達成條件才能拉動(例如在UI中將3*3方格改為相同顏色)
  但是測試玩家(我室友)第一根就花了很久所以取消了

四個房間
  Waiting Room:
    玩家和1個NPC
  Clear Room:
    五隻小怪
    擊殺完生成NPC2
    原本Boss會生在中間的地下空間後來取消了
  Chase Room:
    對應到Dungeon F7 Boss Fight Phase3 Goldor
    四根拉桿對應 Terminal & Device
  Boss Room:
    進入後生成Boss
    所有Enemy清除

遊戲結束房間:
  顯示 Win/Lose 遊玩時間 擊殺數
  與NPC對話直接重新開始(不回Menu)
  參考F7結束效果選擇做這個房間而不是用UI

起始畫面:
  Start:開始遊戲
  How to play:簡單解釋按鍵操作
  Exit:退出遊戲
  右下角 i:顯示製作人

暫停功能(遊戲中ESC開啟)
  Resume: 繼續遊戲
  Menu: 回到主頁面
  和一個音量調節

BGM
  一個BGM Loop
  音樂有剪過成為完美的Loop
