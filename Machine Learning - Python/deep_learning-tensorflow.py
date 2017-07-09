# MNIST For ML Beginners!

# This tutorial is intended for readers who are new to both machine learning and TensorFlow.
# Just like programming has Hello World, machine learning has MNIST.
# MNIST is a simple computer vision dataset. It consists of images of handwritten digits.
# Source: https://goo.gl/rLXVsR

# Please help us to improve this section by sending us your
# feedbacks and comments on: https://docs.google.com/forms/d/16fH20Qf8gJ2o31Vnlss2uLJ7wL9vq76TeUGqghTY0uI/viewform

# Importing input data
import random
import input_data
mnist = input_data.read_data_sets(raw_input(), raw_input(), raw_input())  

# inputs are:     
#   minst.train = data points to train the model 
#   minst.test = data points to test the model 
#   minst.validation =  data points for selecting hyper-parameters like learning rate and size of the model 
# data points formatted as:    minst.train.images, minst.train.labels
# images normalized to 28 x 28 pixels, flatten image for tensor length = 28*28 = 784

# predicted probabilities y = softmax(evidence of label) = softmax(Wx + b)
# x = tensor of image
# W = Weight
# b = bias

import tensorflow as tf
x = tf.placeholder(tf.float32, [None, 784])
W = tf.Variable(tf.zeros([784, 10]))
b = tf.Variable(tf.zeros([10]))

# produce model
y = tf.nn.softmax(tf.matmul(x, W) + b)  

# Use cross entropy to determine difference in the model's predicted probability distribution & actual probability distribution
# cross entropy = Hy'(y) = -Sum[y' * log(y)]  (will take mean of all the Hy'(y) )
# y = predicted probability distibution
# y' = y_ = actual probability distribution 
y_ = tf.placeholder(tf.float32, [None, 10])  # y' placeholder for actual probability distribution to fed in later
cross_entropy = tf.reduce_mean(-tf.reduce_sum(y_ * tf.log(y), reduction_indices=[1]))

# define what happens each step of the training
# minimize the cross_entropy with an optimal learning rate of 0.05
train_step = tf.train.GradientDescentOptimizer(0.05).minimize(cross_entropy)

# Model is complete, launch the model
sess = tf.InteractiveSession()
tf.initialize_all_variables().run() # initialize all the variables created above   (newer versions use: tf.global_variables_initializer().run())

# do the actual training: feed the model 100 of training set's images & labels, do it 1000 times
for _ in range(5000):
    batch_images, batch_labels = mnist.train.next_batch(100)
    sess.run(train_step, feed_dict={x: batch_images, y_: batch_labels})

# print ' '.join(map(str, [random.randint(0,9) for _ in range(len(mnist.validation.images))]))  ignore codingame's random printing of numbers

result = sess.run(tf.argmax(y,1), feed_dict={x: mnist.validation.images})
print ' '.join(map(str, result))
#  Gives about 88-90% accuracy, loop training 5000 times, only increases to ~91% 