using System;
using System.Collections.Generic;

public class NautilusSimulator
{
    public struct Vector2D
    {
        public double x;
        public double y;

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.x + b.x, a.y + b.y);
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.x - b.x, a.y - b.y);
        }

        public static Vector2D operator *(double scalar, Vector2D v)
        {
            return new Vector2D(scalar * v.x, scalar * v.y);
        }
        public static Vector2D operator *(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.x * b.x, a.y * b.y);
        }

        public static double Magnitude(Vector2D v)
        {
            return Math.Sqrt(v.x * v.x + v.y * v.y);
        }
    }

    public static List<Vector2D> SimulateTrajectory(double duration, double dt)
    {

        double g = 9.81;  // m/s^2 (gravité)
        double rhoWater = 1000;  // kg/m^3 (densité de l'eau)
        double nautilusMass = 3000000;  // kg
        double nautilusVolume = 100000;  // m^3
        double nautilusLenght = 100; // m
        double frictionCoefficient = 0.5;  // coefficient de frottement avec l'eau
        double engineThrust = 50000000;  // N
        double variableALaCon = -50;

        Vector2D position = new Vector2D(0, 0);  // mètres
        Vector2D velocity = new Vector2D(0, 0);  // m/s

        List<Vector2D> positions = new List<Vector2D>();
        positions.Add(position);

        double currentTime = 0;

        while (currentTime < duration)
        {
            Vector2D weight = new Vector2D(0, nautilusMass * g);  // poids dirigé vers le bas
            Vector2D archimedes = new Vector2D(0, rhoWater * nautilusVolume * g);  // poussée d'Archimède dirigée vers le haut
            Vector2D friction = -frictionCoefficient * Vector2D.Magnitude(velocity) * velocity;  // frottements dirigés à l'opposé de la vitesse
            Vector2D thrust = new Vector2D(engineThrust, 0);  // poussée des moteurs dirigée vers l'avant
            Vector2D waterWeight = new Vector2D(0, position.y * nautilusLenght * rhoWater); // poids de l'eau au dessus du sous marin
            Vector2D randomALaCon = new Vector2D(NextDouble(variableALaCon, 100), NextDouble(variableALaCon, 100));

            Vector2D totalForce = weight + archimedes + friction + thrust + waterWeight * randomALaCon;
            Vector2D acceleration = (1 / nautilusMass) * totalForce;

            velocity = velocity + dt * acceleration;
            position = position + dt * velocity;

            position.y = position.y / 1.5;

            positions.Add(position);

            currentTime += dt;
        }

        return positions;
    }

    public static void Main(string[] args)
    {
        double duration = 100;  // secondes
        double dt = 0.1;  // secondes

        List<Vector2D> trajectory = SimulateTrajectory(duration, dt);

        // Affichage des résultats
        foreach (var pos in trajectory)
        {
            Console.WriteLine($"x: {pos.x}, y: {pos.y}");
        }
    }

    private static int seed = Environment.TickCount;

    // Génère un nombre pseudo-aléatoire entre 0 et 1.
    public static double NextDouble()
    {
        const int a = 1664525;
        const int c = 1013904223;
        const int m = 0x7FFFFFFF; // Int32.MaxValue

        seed = (a * seed + c) % m;

        return (double)seed / m;
    }

    // Génère un nombre pseudo-aléatoire dans une plage donnée [minValue, maxValue).
    public static double NextDouble(double minValue, double maxValue)
    {
        return minValue + (maxValue - minValue) * NextDouble();
    }
}
