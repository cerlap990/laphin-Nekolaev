using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerController : MonoBehaviour
{

    private float moveSpeed = 6.0f;
    private float jumpHeight = 10.0f;
    public float smoothTime = 0.1f; // Время сглаживания движения
    private Vector2 velocity = Vector2.zero; // Текущая скорость
    public LayerMask groundLayer;



    private Rigidbody2D rb;


   private int actualSprite = 0;
	private float spriteInterval = 0.1f;
	private Sprite[] sprites = new Sprite[2];
	private Sprite[] crouchingSprites = new Sprite[2];

	private String genomeBasePath = "Genomes/genome_";

	[SerializeField]
	private List<Cactus> cactus = new List<Cactus> ();

	[SerializeField]
	private List<Jumped> jumps = new List<Jumped>();

	private List<Genome> genomes = Utils.loadAllGenomes ();

	private bool isGrounded = true;
	private bool isCrouching = false;

	private int actualJumpGenome = 0;

	private bool isLearning = true;

    private Animator animator;


    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;

        // Получаем компонент Animator
        animator = GetComponent<Animator>();

        isLearning = (genomes.Count < 4);
        if (!isLearning && Utils.actualGenome >= genomes.Count)
        {
            print("Acabou de jogar os genomas");
            Utils.clearCrossOversFolder();
            genomes = Utils.loadAllGenomes(); //Force GENOMES root folder

            List<Genome> bestGenomes = Utils.naturalSelection(genomes, 4);
            Utils.clearGenomesFolder();

            for (int i = 0; i < bestGenomes.Count; i++)
            {
                Utils.persistInJson(bestGenomes[i], genomeBasePath + i + "_");
            }

            //New Crossovers + Mutations
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    Genome g = Genetic.crossOver(bestGenomes[i], bestGenomes[j]);
                    g = Genetic.mutate(g);
                    Utils.persistInJson(g, "Genomes/CrossedOvers/genome_" + i + "_" + j + "_");
                }
            }

            genomes = Utils.loadAllGenomes();
            Utils.actualGenome = 0;
        }
        sprites = Resources.LoadAll<Sprite>("Art/Player/Standing");
        crouchingSprites = Resources.LoadAll<Sprite>("Art/Player/Crouching");

        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;

        //Load Cactus
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("cactus"))
        {
            int cacType;
            switch (c.name)
            {
                case "cactus_1":
                    cacType = 1;
                    break;
                case "cactus_2":
                    cacType = 2;
                    break;
                case "cactus_3":
                    cacType = 3;
                    break;
                default:
                    cacType = 1;
                    break;
            }
            Cactus toAdd = new Cactus()
            {
                type = cacType,
                position = c.transform.position
            };
            cactus.Add(toAdd);
        }
    }


    void Update()
    {
        // Движение персонажа в сторону с заданной скоростью
        float targetVelocityX = moveSpeed;
        velocity.x = Mathf.Lerp(velocity.x, targetVelocityX, smoothTime);

        // Применение новой скорости к Rigidbody2D
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, GetComponent<Rigidbody2D>().velocity.y);

        // Обработка прыжка
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            // Применение силы прыжка
            velocity.y = jumpHeight; // Применение прыжка к вертикальной скорости
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y);
            isGrounded = false; // Установка состояния "не на земле"
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name.StartsWith("cactus"))
        {
            GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;

            Genome genome = new Genome
            {
                fitness = Genetic.calculateFitness(jumps, cactus),
                jumps = jumps
            };

            Utils.persistInJson(genome, genomeBasePath);

            jumps.Clear();

            if (!isLearning)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1;
                Utils.actualGenome++;
            }
        }
        else if (coll.gameObject.name.StartsWith("Ground"))
        {
            isGrounded = true;
        }
    }

    Cactus getNextNearestCactus()
    {
        float nearestDist = float.PositiveInfinity;
        Cactus nearestCactus = null;
        foreach (Cactus c in cactus)
        {
            float cacX = c.position.x;
            float playerX = GetComponent<Rigidbody2D>().position.x;
            if (cacX > playerX)
            {
                float dist = cacX - playerX;
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearestCactus = c;
                }
            }
        }

        return nearestCactus;
    }

    void playGenome(int genomeIndex)
    {
        float dist = getNextNearestCactus().position.x - GetComponent<Rigidbody2D>().position.x;
        if (actualJumpGenome >= genomes[genomeIndex].jumps.Count)
        {
        }
        else if (dist <= genomes[genomeIndex].jumps[actualJumpGenome].distanceToNearestCactus.x && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            isGrounded = false;

            Cactus c = getNextNearestCactus();
            Jumped jump = new Jumped
            {
                nearestCactus = c,
                distanceToNearestCactus = c.position - GetComponent<Rigidbody2D>().position
            };

            jumps.Add(jump);

            actualJumpGenome++;
        }
    }




}