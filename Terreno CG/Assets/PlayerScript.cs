using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float velocidade = 7f;
	public float gravidade = -9.8f;
	Vector3 aceleracao;
	public GameObject portaAberta;
	public GameObject portaFechada;
	GameObject tap;
	public GameObject agua;
	[SerializeField]
	private GameObject[] interruptor;
	[SerializeField]
	private GameObject[] luz;
	public CharacterController controller;

	public Transform groundCheck;
	public float distanciaDoChao = 0.4f;
	public float distanciaPorta = 1f;
	public LayerMask groundMask;
	bool isGrounded;
    // Start is called before the first frame update
    void Start()
	{
		tap = GameObject.FindGameObjectWithTag("Tap");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, distanciaDoChao, groundMask);

		if(isGrounded && aceleracao.y < 0){
			aceleracao.y = -2f;
		}

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 inputDirection = transform.right * x + transform.forward * z;
		aceleracao.y += gravidade * Time.deltaTime;
        controller.Move(inputDirection * velocidade * Time.deltaTime);
        controller.Move(aceleracao * Time.deltaTime);

		Interagir();

    }

	void Interagir(){
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(Vector3.Distance(transform.position, portaAberta.transform.position) < distanciaPorta){
				if(portaAberta.activeSelf){
					portaAberta.SetActive(false);
					portaFechada.SetActive(true);
				}else if(portaFechada.activeSelf){
					portaFechada.SetActive(false);
					portaAberta.SetActive(true);
				}
			}
			for(int i = 0; i < interruptor.Length; i++){
				if(Vector3.Distance(transform.position, interruptor[i].transform.position) < 1){
					if(luz[i].activeSelf){
						luz[i].SetActive(false);
					}else{
						luz[i].SetActive(true);
					}
				}	
			}

			if(Vector3.Distance(transform.position, tap.transform.position) < 1){
				if(agua.activeSelf){
					agua.SetActive(false);
				}else{
					agua.SetActive(true);
				}
			}

		}
	}
}
