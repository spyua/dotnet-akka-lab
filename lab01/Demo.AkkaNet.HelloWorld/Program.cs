﻿using System;
using Akka.Actor;
using Demo.AkkaNet.HelloWorld.Actors;
using Demo.AkkaNet.HelloWorld.Messages;

namespace Demo.AkkaNet.HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicActorCreationUsingTell();

//            BasicActorCreationUsingAsk();
        }

        static void BasicActorCreationUsingTell()
        {
            ActorSystem system = ActorSystem.Create("my-first-akka");

            IActorRef untypedActor = system.ActorOf<MyUntypedActor>("untyped-actor-name");
            IActorRef typedActor = system.ActorOf<MyTypedActor>("typed-actor-name");

            untypedActor.Tell(new GreetingMessage("Hello untyped actor!"));
            typedActor.Tell(new GreetingMessage("Hello typed actor!"));

            for (var i = 0; i < 100; i++)
            {
                typedActor.Tell("test" + i);
            }

            Console.Read();
            system.Terminate();
        }

        static void BasicActorCreationUsingAsk()
        {
            ActorSystem system = ActorSystem.Create("calc-system");

            IActorRef calculator = system.ActorOf<CalculatorActor>("calculator");

            for (var i = 0; i < 10; i++)
            {
                AnswerMessage result = calculator.Ask<AnswerMessage>(new AddMessage(i, 1)).Result;

                Console.WriteLine("Addition result: " + result.Value);
            }

            Console.Read();
            system.Terminate();
        }
    }
}