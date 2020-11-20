using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ThirtyDayWorkoutChallenge
{
    public class Function
    {
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            ILambdaLogger log = context.Logger;
            log.LogLine($"Skill Request Object:" + JsonConvert.SerializeObject(input));

            Session session = input.Session;
            if (session.Attributes == null)
                session.Attributes = new Dictionary<string, object>();

            Type requestType = input.GetRequestType();
            if (requestType == typeof(LaunchRequest))
            {
                string speech = "Welcome to the 30 day fitness challenge. Choose from a variety of workouts that increase in intensity each day. You choose the workouts that suit you. Shall we begin?";
                Reprompt rp = new Reprompt("Are you ready to start the challenge?");
                return ResponseBuilder.Ask(speech, rp, session);
            }
            else if (requestType == typeof(SessionEndedRequest))
            {
                return ResponseBuilder.Tell("Goodbye!");
            }
            else if (requestType == typeof(IntentRequest))
            {
                var intentRequest = (IntentRequest)input.Request;
                switch (intentRequest.Intent.Name)
                {
                    case "AMAZON.CancelIntent":
                    case "AMAZON.StopIntent":
                        return ResponseBuilder.Tell("Goodbye!");
                    case "AMAZON.HelpIntent":
                        {
                            Reprompt rp = new Reprompt("What's next?");
                            return ResponseBuilder.Ask("Here's some help. What's next?", rp, session);
                        }
                    case "SelectNameIntent":
                        {
                            //Ask user for their name.
                            //Save name as a session attribute
                            //Save name to database at the end of the session

                            string next = "Guess a number betwen 1 and 10";
                            Reprompt rp = new Reprompt(next);
                            return ResponseBuilder.Ask(next, rp, session);
                        }
                    case "ChooseSetIntent":
                        {
                            string speech = "Okay. It is time to tailor your workouts for the next 30 days. " +
                                "You can create a Custom set Or you can choose one of our Premade sets professionally " +
                                "formulated by a fitness trainer. Which would you prefer?";

                            string reprompt = "Would you prefer a premade set or make your own with a custom set?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "PremadeSetIntent":
                        {
                            string speech = "Okay. Choose from 'Cardio', 'Strength' or 'Full Body. If you would like more information, say the name of the set and more info.";

                            string reprompt = "Which workout do you want to do? Cardio, Strength or Full Body?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "CardioIntent":
                        {
                            string speech = "Okay. Cardio consists of 5 high energy workouts to get your heart pumping. Would you like to begin?";

                            string reprompt = "Cardio consists of 5 workouts. Are you ready to begin or would you prefer to choose a different workout?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "FirstWorkoutIntent":
                        {
                            //Pull day 1 workout 1, for cardio out of the database. 
                            //Workout: Jumping Jacks
                            //Day 1: 10

                            session.Attributes["firstWorkout"] = "Jumping Jacks";
                            session.Attributes["firstWorkoutAmount"] = 10;

                            string firstWorkout = (string)session.Attributes["firstWorkoutAmount"] + " " + session.Attributes["firstWorkout"];

                            string speech = "Welcome to day 1 of the 30 day workout challenge. The first exercise is " + firstWorkout + ". Shall we begin?";

                            string reprompt = "The first exercise is  " + firstWorkout + ". Are you ready to begin?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "BeginFirstWorkoutIntent":
                        {
                            string firstWorkout = (string)session.Attributes["firstWorkoutAmount"] + " " + session.Attributes["firstWorkout"];

                            string speech = "Okay. Take a break if you need it. Its time for " + firstWorkout + 
                                " Once you have completed this. Say my name and finished. Ready... lets go";

                            //Play music for one minute
                            //Reprompt user to check if they are done
                            string reprompt = "Are you finished or would you like more time?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "SecondWorkoutIntent":
                        {
                            //Pull day 1, workout 2, for cardio out of the database. 
                            //Workout: Squats
                            //Day 1: 10

                            session.Attributes["secondWorkout"] = "Squats";
                            session.Attributes["secondWorkoutAmount"] = 10;

                            string secondWorkout = (string)session.Attributes["secondWorkoutAmount"] + " " + session.Attributes["secondWorkout"];

                            string speech = "Welcome to day 1 of the 30 day workout challenge. The second exercise is " + secondWorkout + ". Shall we begin?";

                            string reprompt = "The second exercise is " + secondWorkout + ". Are you ready to begin?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "BeginSecondWorkoutIntent":
                        {
                            string firstWorkout = (string)session.Attributes["firstWorkoutAmount"] + " " + session.Attributes["firstWorkout"];

                            string speech = "Okay. Take a break if you need it. Its time for " + firstWorkout +
                                " Once you have completed this. Say my name and finished. Ready... lets go";

                            //Play music for one minute
                            //Reprompt user to check if they are done
                            string reprompt = "Are you finished or would you like more time?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "ThirdWorkoutIntent":
                        {
                            //Pull day 1 workout for cardio out of the database. 
                            //Workout: Jumping Jacks
                            //Day 1: 10

                            session.Attributes["firstWorkout"] = "Jumping Jacks";
                            session.Attributes["firstWorkoutAmount"] = 10;

                            string firstWorkout = (string)session.Attributes["firstWorkoutAmount"] + " " + session.Attributes["firstWorkout"];

                            string speech = "Welcome to day 1 of the 30 day workout challenge. The first exercise is " + firstWorkout + ". Shall we begin?";

                            string reprompt = "The first exercise is 10 jumping jacks. Are you ready to begin?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "BeginThirdWorkoutIntent":
                        {
                            string firstWorkout = (string)session.Attributes["firstWorkoutAmount"] + " " + session.Attributes["firstWorkout"];

                            string speech = "Okay. Take a break if you need it. Its time for " + firstWorkout +
                                " Once you have completed this. Say my name and finished. Ready... lets go";

                            //Play music for one minute
                            //Reprompt user to check if they are done
                            string reprompt = "Are you finished or would you like more time?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "FourthWorkoutIntent":
                        {
                            //Pull day 1 workout for cardio out of the database. 
                            //Workout: Jumping Jacks
                            //Day 1: 10

                            session.Attributes["firstWorkout"] = "Jumping Jacks";
                            session.Attributes["firstWorkoutAmount"] = 10;

                            string firstWorkout = (string)session.Attributes["firstWorkoutAmount"] + " " + session.Attributes["firstWorkout"];

                            string speech = "Welcome to day 1 of the 30 day workout challenge. The first exercise is " + firstWorkout + ". Shall we begin?";

                            string reprompt = "The first exercise is 10 jumping jacks. Are you ready to begin?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "BeginFourthWorkoutIntent":
                        {
                            string firstWorkout = (string)session.Attributes["firstWorkoutAmount"] + " " + session.Attributes["firstWorkout"];

                            string speech = "Okay. Take a break if you need it. Its time for " + firstWorkout +
                                " Once you have completed this. Say my name and finished. Ready... lets go";

                            //Play music for one minute
                            //Reprompt user to check if they are done
                            string reprompt = "Are you finished or would you like more time?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "FifthWorkoutIntent":
                        {
                            //Pull day 1 workout for cardio out of the database. 
                            //Workout: Jumping Jacks
                            //Day 1: 10

                            session.Attributes["firstWorkout"] = "Jumping Jacks";
                            session.Attributes["firstWorkoutAmount"] = 10;

                            string firstWorkout = (string)session.Attributes["firstWorkoutAmount"] + " " + session.Attributes["firstWorkout"];

                            string speech = "Welcome to day 1 of the 30 day workout challenge. The first exercise is " + firstWorkout + ". Shall we begin?";

                            string reprompt = "The first exercise is 10 jumping jacks. Are you ready to begin?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "BeginFifthWorkoutIntent":
                        {
                            string firstWorkout = (string)session.Attributes["firstWorkoutAmount"] + " " + session.Attributes["firstWorkout"];

                            string speech = "Okay. Take a break if you need it. Its time for " + firstWorkout +
                                " Once you have completed this. Say my name and finished. Ready... lets go";

                            //Play music for one minute
                            //Reprompt user to check if they are done
                            string reprompt = "Are you finished or would you like more time?";

                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "CustomSetIntent":
                        {
                            // check answer
                            string userString = intentRequest.Intent.Slots["Number"].Value;
                            Int32 userInt = 0;
                            Int32.TryParse(userString, out userInt);
                            bool correct = (userInt == (Int32)(long)session.Attributes["magic_number"]);
                            Int32 numTries = (Int32)(long)session.Attributes["num_guesses"] + 1;
                            string speech = "";
                            if (correct)
                            {
                                speech = "Correct! You guessed it in " + numTries.ToString() + " tries. Say new game to play again, or stop to exit. ";
                                session.Attributes["num_guesses"] = 0;
                            }
                            else
                            {
                                speech = "Nope, guess again.";
                                session.Attributes["num_guesses"] = numTries;
                            }
                            Reprompt rp = new Reprompt("speech");
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    case "MoreInfoIntent":
                        {
                            string speech = "Okay. Choose from 'Cardio', 'Strength' or 'Full Body. If you would like more information, say the name of the set and more info.";

                            string reprompt = "Which workout do you want to do? Cardio, Strength or Full Body?";
                            Reprompt rp = new Reprompt(reprompt);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                    default:
                        {
                            log.LogLine($"Unknown intent: " + intentRequest.Intent.Name);
                            string speech = "I didn't understand - try again?";
                            Reprompt rp = new Reprompt(speech);
                            return ResponseBuilder.Ask(speech, rp, session);
                        }
                }
            }
            return ResponseBuilder.Tell("Goodbye!");
        }
    }
}
