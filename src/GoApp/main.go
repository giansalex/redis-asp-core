package main

import (
	"fmt"

	"github.com/gomodule/redigo/redis"
)

const channelName = "ASP_CORE_CHAN"

func main() {
	c, err := redis.DialURL("redis://localhost:6379")
	if err != nil {
		fmt.Println("No se pudo connectar" + err.Error())
		return
	}
	defer c.Close()
	fmt.Println("Connectado")
	psc := redis.PubSubConn{Conn: c}
	if err := psc.Subscribe(redis.Args{}.AddFlat(channelName)...); err != nil {
		fmt.Println("No se pudo Subscribir" + err.Error())
		return
	}

	done := make(chan error, 1)

	// Start a goroutine to receive notifications from the server.
	go func() {
		for {
			switch n := psc.Receive().(type) {
			case error:
				done <- n
				return
			case redis.Message:
				fmt.Printf("channel: %s, message: %s\n", n.Channel, n.Data)

				// For the purpose of this example, cancel the listener's context
				// after receiving last message sent by publish().
				if string(n.Data) == "goodbye" {
					done <- nil
				}
			}
		}
	}()

	<-done

	psc.Unsubscribe()
}
