# Chaturanga

Welcome to Chaturanga! The war room simulator that puts you in command of the most powerful and notorious militaries in history!

Chaturanga is a War strategy game that takes famous battles in history and puts the user in the perspective of the actual winner. The users goal is to make strategic decisions that help their army win the battle. There is no actual shooter gameplay, it is purely strategy and timing. The graphics are intended to be sleek and resemble a futuristic war room 3D map. Battle progress and regiment statistics are displayed using various statistics on the icons on the map. Strategy options are offered to the user in real time, as well as some moves that are available to the user at all times (except when restricted). Different armies have different personnel and strategies, and every battle has a unique armies and terrain. The user will not be informed of the actual historical battle until after the fight. Each level will increase in strategic or probable difficulty. There are several battle types with principle battle objectives that must be completed in order to beat the level, including: ambush, siege, last stand. The user will assume the role of the commander of the army for that particular battle and will have access to several key personnel that each have a strategic purpose in battle. These personnel are somewhat historically accurate, with some unconfirmed tales of legendary units included to add an extra element of fun. 

The game algorithm is structured so that each level can be beaten using two general strategies:
1. By consistently making good battle decisions according to the BattleSim program, which uses my own personal notes from the Art of War(translated) by Sun-Tzu and the Correlation of Forces and Means Algorithm (COFM) developed by the United States Military, theoretically providing other ways the battle could have been won from the user’s side, possibly alternative to the historical account.
2. By consistently making decisions that follow the historical accounts, and lead to the actual outcome of the battle (based on my own research) which is an allied victory.
A combined score is calculated from each of these programs and monitored by the GameManager, which triggers a battle win and level completion or battle loss and level retry, if the combined score passes a certain upper or lower threshold.

After a certain number of retries the user will also be given a hint, which they can choose to either use to gain a historical clue about the battle if they want to determine what battle it is, or a clue providing general strategic advice based on the combating armies, terrain, weather, etc.
