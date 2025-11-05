using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Vitesse de déplacement du joueur
    private Rigidbody2D rb;
    private Vector2 movementDirection;     // Direction du mouvement selon les entrées clavier


    void Start()
    {
        // On récupère le Rigidbody2D attaché à l'objet du joueur
        rb = GetComponent<Rigidbody2D>();

        // pour faire spawn le joueur sur la map
        GameObject spawn = GameObject.Find("SpawnPoint");
        if (spawn != null)
        {
            transform.position = spawn.transform.position;
        }

    }

    private void Update()
    {
        // On récupère les entrées de déplacement des flèches directionnelles
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        if ( rb != null)
        {
            rb.linearVelocity = movementDirection * moveSpeed; // multiplication de la direction par la vitesse choisie
        }
        
    }
}