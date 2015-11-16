###Incomplete Knowledge Game Playing With Feedforward Neural Networks and Competitive Co-Evolution
BY: Damian Polan - 100825303
Credit to Alex Monette for cooperation in designing the game portion of this project.

####Problem Description and Domain

The problem designed for the project is intended to give insight in the domain of game playing AI with incomplete knowledge. In this case a custom game was designed and implemented.

The game board consists of an N by N grid with passable and impassable squares. There are two players, each with their own set of pieces. The pieces are set in a predefined location (setup image on the right). Each player begins with 5 pieces: 4 pawn and one king. Red denotes player one, dark red is the king for player one. Blue is player two, dark blue for the king for player two. 
Pawns can choose to do one of three things each turn: attack adjacent enemy piece, move to adjacent square, or can remain stationary and defend for the turn. 
If a piece defends for a turn, a total of 1 incoming damage will be blocked for that turn.
Each piece/king can pick two of these to do each turn. I.e. Move right. attack up. or attack up, attack up.
The king can choose to either move to adjacent square. do nothing, or attack an enemy piece at any direct horizontal or vertical distance.This means the king is able to deal damage to enemy pieces from across the map and outside player vision. 
An attack from any piece (king or pawn) deals a total of one damage to the targeted piece. Each piece starts with 4 health. When a piece’s health reaches 0, the piece is removed from the board.
When the king reaches zero health the game ends and the player with the surviving king wins.
To differentiate the AI required for this problem from a game like chess, an extra component is added: A given player only has vision of the board for squares within a certain range of each of that players pieces.
This adds incomplete knowledge to the game and thus requires a different approach for creating an artificial intelligence to play the game.


####Motivation for the Problem

The game problem described above proposes a simple set of rules that add a challenging dynamic for an artificial intelligence solution. It pulls from traditional 1v1 games such as chess and checkers in that the players take turns with a finite set of possible moves in each turn. But one aspect which adds a number of interesting challenges is vision. The idea that the entire state of the board will not always be known at any given time. This scenario is known as incomplete information. Another is the ability to move multiple pieces at the same time where it can be advantageous to move them in a coordinated fashion.

What sort of insight can solving this game bring us to real world applications? 
Many robotic systems that must interact with a changing and unknown environment. For example, multiple robot vacuum cleaners can be placed in a large unknown room with the intent to map and clean it. Solving a game with a multi agent system and multiple unknowns can give insight to solving such a problem.

####AI Techniques Used

Due to the nature of the problem, and AI algorithm that can handle incomplete knowledge and undetermined states should be used. Optimality, the algorithm should be able to decide moves based on the current game state AND previous game states seen. For example, if the enemy king just left the AI’s vision, it should be able to remember what area of the board it last saw the king and evaluate its next move to compensate. The idea of vision can pose problems for simple algorithms that require complete knowledge to choose a move efficiently. 

For the proposed project, a focus on Artificial Neural Network’s (ANN’s) is made. There exists a large variety of neural network algorithms that can be applied to this problem. For instance, a Recurrent Neural Network (RNN) incorporates internal directed cycles which allow it temporal knowledge of a game state. The problem with the RNN is that it is difficult to train for networks with a large number of nodes. As it will be seen later, this problem involves a large number of input nodes meaning RNN will not be a sufficient choice.

The chosen network for this project is a Feedforward Neural Network (FNN). A FNN is organized in a one directional fashion. It takes a set of input values, propagates them through one or more layers of neurons with activation functions, and outputs a set of values. The feedforward neural network does not incorporate any temporal aspects so it cannot remember previous states. To combat this problem, an added set of inputs can be integrated into the FNN representing locations of pieces not seen last turn but seen now. This adds a higher input count but increases the knowledge for that state.

For training the FNN, a genetic algorithm is used in a competitive co-evolution. A competitive co-evolution is a back and forth training system where two separate populations are trained against each other in an alternating sequence. One ‘host’ population is trained with the goal of increasing a game-winning based heuristic as much as possible. The other ‘parasite’ population trained in parallel with the goal of minimizing or counteracting the ‘host’ populations goals. The host and parasite population will alternate roles throughout the training.

The reason a FNN algorithm is a good choice for this problem is due to its simplicity to implement and its ability for quick decision making. Once the FNN is trained, evaluating a position can be done almost instantly by feeding the input values into the FNN and parsing the output data.


Supervised (back propagation)... unrealistic to guide by human, would take too many trials. Can guide by heuristic evaluation of the next state (drawback is high processing because evaluating every move).


####Design

Feedforward Neural Network
For simple FNN implementation, the AForge.net C# library is used. The library provides a set of useful neural network classes and functions. An activation record is used

The most challenging part of this project was deciding on what data to feed into the FNN and how to feed it. In a large game like this, there is a lot of information that needs to be translated into neural network friendly float data. One (bad) way to do it is to create a large array of inputs for the neural network including the piece state of every position on the board. Including the health of the piece at specific locations and the piece info would require even more nodes!

A number of things are done to simplify the incoming data. Instead of considering the AI as one big hole that plays for the whole team, the AI is broken down into two different Neural networks. One is for deciding the King pieces moves and the other is deciding Pawn piece moves. The single neural network for the Pawns is reused for each Pawn on the board. The incoming data for a specific piece is relativized such that it is seen from the pawns perspective. I.e. all coordinates are converted to relative ones. 

Even with relativized data it is still difficult to bind the data to the neural network in an effective way. The position of enemy pieces around a specific player piece must be known to allow for the neural network to make a reasonable decision. 

For simplicity, an approximation is used for knowledge of piece position.The approximation determines the positions of the pieces around the player piece with just 4 input values. Each generated input value represents the distance of the closest enemy node at a certain direction relative to the player piece. 4 inputs for 4 directions: North, East, West, and South.

The input float value is defined by a combination of Manhattan distance and inverse function such that the nodes directly adjacent to the player piece are represented by an input value of 1.0. Pieces far away will tend toward 0. Each unit of Manhattan distance away from the piece halves the value of the input float. This allows for closer pieces to be more strongly considered than further pieces.
The float value F(p) is modeled by: 
F(p) =  1 / (2^(ManhattenDist(p) - 1)), where p is the relative position of the closest enemy piece for a given direction.

This method is also extrapolated for relative team positions and block positions.

#####Structure
The layering and structure of the neural network is crucial to good population training results. If the input data is linearly separable, only a single hidden layer is needed. In this case, it is not known if the best solution for the data is linearly separable so two hidden layers are used to compensate. Both hidden layers sizes are linearly interpolated between the number of inputs and number of outputs.

#####Outputs
For both moves each turn, the FNN must cover: Left, Right, Up, Down, Attack_Left, Attack_Right, Attack_Up, Attack_Down, Defend moves. To do this, two neurons for each attack are used. Four total neurons.
2 Neurons for each attack:
1 output for direction: 0 to 1.0
0 to 0.25	N (i.e. the below action will be be done in the North direction)
0.25 to 0.5	E
0.5 to 0.75	S
0.75 to 1.0	W
1 output for action(defined in the same way): 
Move
Attack
Defend



#####Training
The ‘dream’ way to train the neural network would be to take a large set of known played games where the best possible moves are played every game and train the neural network based on those. Unfortunately it is almost impossible to get a set like that for obvious reasons: it is very difficult to know what the best move is and it would take a very long time for a person to compile a large set of training data.

In this case, the competitive co-evolution technique is used. Two neural networks are directly competed against each other with one of them being evaluated for training. After a few generations of training, the neural network being evaluated is expected to win more games than the other. At this point, the roles are swapped and the other neural networks becomes the evaluated target. The process continues until a reasonable amount of training is completed. 
A training run is made which consists of the following properties:
host and parasite populations
individual generations for host and the parasite
a size of 20 for each generation
the game is played by each genome in the 20 generations and its fitness is evaluated by a heuristic function. The game has a maximum moves of 50 to prevent infinite games.
the two best genomes are selected for creating the next generation
after each generation the host and parasite swap roles and the next generation is tested
continues for 1000 trials.

#####Heuristic
Two separate heuristics are used. 
One for the host population is calculated:
weighted sum of all the teams health points: +100 for each health point. +500 for king health points
weighted subtraction of enemy health points: -150 for each health point. -500 for king
if the host wins the game the heuristic gets a +10000 bonus. If it loses, -10000
a distance traveled +1 per distance per piece is added to allow the genetic algorithm to kickstart into action.
One for the parasite population is calculated:
weighted sum of all the teams health points: +100 for each health point. +500 for king health points
weighted subtraction of enemy health points: -150 for each health point. -500 for king
if the parasite wins the game the heuristic gets a +10000 bonus. If it loses, -10000
a distance traveled +1 per distance per piece


####Results

Over thousands of generations of running this algorithm a significant improvement in play is not seen. For the most part pawns and kings are seen just walking into corners and not doing much. At some points in the evolution process, the King NN would automatically fire every turn, allowing it to get some free hits on the opposing team’s pieces and sometimes even king. However, over a few generation this behaviour was lost. 

The loss may be due to the mutation rate applied to the NN. The mutation rate for new generations was randomized with a %70 chance of happening, a +- 10% change to the node would be made. In future experiments it would be beneficial to run a variety of different values and see how they compare against each other.

####Possible Enhancements

One structural enhancement that can be made to the system would be to give each individual pawn its own neural network. This would allow for ‘role’ behaviors were different pawns become responsible for different tasks throughout the game.

Multi threading can be used to speed up evolution time. As it stands right now, to get through 1000 generations takes about 5 minutes. With multithreading that can be cut down by at least %50, depending on the CPU.

Optimization - Pruning, getting rid of pointless nodes. Can apply pruning during training. Start with a large network and prune to a smaller, more efficient, network. 

Evolve the topology of the network, i.e the genetic algorithm can add/remove new nodes

####References

https://en.wikipedia.org/wiki/Recurrent_neural_network
https://en.wikipedia.org/wiki/Artificial_neural_network
https://en.wikipedia.org/wiki/Feedforward_neural_network

Book:
http://hagan.okstate.edu/NNDesign.pdf#page=469

Competative co-evloution http://nn.cs.utexas.edu/downloads/papers/lubberts.coevolution-gecco01.pdf
Library used:
http://www.aforgenet.com/


#####Execution

run COMP4106_ProjFinal\4106_NewMerge\4106_NewMerge\bin\Debug\4106_NewMerge.exe in the terminal. Windows only.
